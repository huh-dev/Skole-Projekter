using Eventyrerlauget.CharacterTypes;
using Eventyrerlauget.DiceRoller;
using Eventyrerlauget.Interfaces;
using Eventyrerlauget.Inventory;

namespace Eventyrerlauget;

public class Program
{
    private const int MinPartySize = 1;
    private const int MaxPartySize = 3;
    private const int StartingLevel = 1;
    private const int MonsterDamage = 6;
    private const int MonsterArmorClass = 12;
    private const int MonsterMaxHp = 40;

    public static void Main(string[] args)
    {
        SetupGame();
    }

    private static void SetupGame()
    {
        IDiceRoller dice = new RandomDiceRoller();
        Party party = CreateParty();
        FightBattle(party, dice);
    }

    private static Party CreateParty()
    {
        Console.WriteLine("=== Character Creator ===");
        int partySize = ReadInt($"Party size ({MinPartySize}-{MaxPartySize}): ", MinPartySize, MaxPartySize);
        var members = new List<Character>();

        for (int i = 0; i < partySize; i++)
        {
            Console.WriteLine($"\nMember {i + 1}");
            members.Add(CreateCharacter());
        }

        return new Party(members);
    }

    private static Character CreateCharacter()
    {
        string name = ReadName();
        Console.WriteLine("1) Warrior  2) Thief  3) Priest  4) Wizard");
        int classChoice = ReadInt("Class: ", 1, 4);

        return classChoice switch
        {
            1 => new Warrior(name, StartingLevel, 32, new Weapon(8), new Armor(14)),
            2 => new Thief(name, StartingLevel, 24, new Weapon(6), new Armor(12)),
            3 => new Priest(name, StartingLevel, 26, new Weapon(6), new Armor(13)),
            _ => new Wizard(name, StartingLevel, 18, new Weapon(4), new Armor(11))
        };
    }

    private static void FightBattle(Party party, IDiceRoller dice)
    {
        var monster = new Monster("Goblin", MonsterDamage, MonsterArmorClass, MonsterMaxHp);
        Console.WriteLine($"\n=== Battle: a {monster.Name} appears! ===");

        while (monster.CurrentHp > 0 && HasLivingMembers(party))
        {
            PrintStatus(party, monster);

            foreach (Character member in party.Members)
            {
                if (member.CurrentHp <= 0 || monster.CurrentHp <= 0)
                {
                    continue;
                }

                TakePlayerTurn(member, party, monster, dice);
            }

            if (monster.CurrentHp > 0 && HasLivingMembers(party))
            {
                TakeMonsterTurn(party, monster, dice);
            }
        }

        Console.WriteLine(monster.CurrentHp <= 0
            ? $"\n{monster.Name} is defeated. You win!"
            : $"\nThe party has fallen. {monster.Name} wins.");
    }

    private static void TakePlayerTurn(Character member, Party party, Monster monster, IDiceRoller dice)
    {
        bool canCast = member is ISpellCaster;
        Console.WriteLine($"\n{member.Name} ({member.GetType().Name}) HP {member.CurrentHp}/{member.MaxHp}");
        Console.WriteLine(canCast ? "1) Attack  2) Cast spell" : "1) Attack");
        int action = ReadInt("Action: ", 1, canCast ? 2 : 1);

        if (action == 2 && member is ISpellCaster caster)
        {
            if (member is Priest)
            {
                Character healTarget = PickHealTarget(party, member);
                int hpBefore = healTarget.CurrentHp;
                caster.CastSpell((IDamageable)healTarget, dice);
                PrintSpellResult(member, healTarget.Name, healTarget.CurrentHp - hpBefore);
                return;
            }

            int monsterHpBeforeSpell = monster.CurrentHp;
            caster.CastSpell(monster, dice);
            PrintSpellResult(member, monster.Name, monster.CurrentHp - monsterHpBeforeSpell);
            return;
        }

        int monsterHpBefore = monster.CurrentHp;
        member.Attack(monster, dice);
        PrintAttackResult(member.Name, monster.Name, monsterHpBefore, monster.CurrentHp);
    }

    private static void TakeMonsterTurn(Party party, Monster monster, IDiceRoller dice)
    {
        List<Character> living = LivingMembers(party);
        Character target = living[dice.RollDice(living.Count) - 1];
        int hpBefore = target.CurrentHp;
        monster.Attack((IDamageable)target, dice);
        PrintAttackResult(monster.Name, target.Name, hpBefore, target.CurrentHp);
    }

    private static Character PickHealTarget(Party party, Character caster)
    {
        Character? wounded = party.Members.Find(member =>
            member.CurrentHp > 0 && member.CurrentHp < member.MaxHp);

        return wounded ?? caster;
    }

    private static void PrintSpellResult(Character caster, string target, int change)
    {
        if (change > 0)
        {
            Console.WriteLine($"{caster.Name} heals {target} for {change}.");
            return;
        }

        if (change < 0)
        {
            Console.WriteLine($"{caster.Name} blasts {target} for {-change}.");
            return;
        }

        Console.WriteLine($"{caster.Name}'s spell has no effect.");
    }

    private static void PrintAttackResult(string attacker, string defender, int hpBefore, int hpAfter)
    {
        int damage = hpBefore - hpAfter;
        Console.WriteLine(damage > 0
            ? $"{attacker} hits {defender} for {damage}."
            : $"{attacker} misses {defender}.");
    }

    private static void PrintStatus(Party party, Monster monster)
    {
        Console.WriteLine($"\n{monster.Name} HP {monster.CurrentHp}/{monster.MaxHp}");
        foreach (Character member in party.Members)
        {
            string state = member.CurrentHp <= 0 ? " (down)" : "";
            Console.WriteLine($"- {member.Name} ({member.GetType().Name}) HP {member.CurrentHp}/{member.MaxHp}{state}");
        }
    }

    //MARK: - Helper methods

    private static bool HasLivingMembers(Party party)
    {
        return LivingMembers(party).Count > 0;
    }

    private static List<Character> LivingMembers(Party party)
    {
        return party.Members.FindAll(member => member.CurrentHp > 0);
    }

    private static string ReadName()
    {
        while (true)
        {
            Console.Write("Name: ");
            string? name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
            {
                return name.Trim();
            }

            Console.WriteLine("Name cannot be empty.");
        }
    }

    private static int ReadInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
            {
                return value;
            }

            Console.WriteLine($"Enter a number between {min} and {max}.");
        }
    }
}
