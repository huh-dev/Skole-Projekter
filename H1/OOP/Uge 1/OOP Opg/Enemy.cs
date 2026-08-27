namespace OOP
{

    internal class Enemy : ActorBase
    {
        private string[] names = { "Nørd1", "Nørd2", "Nørd3", "Nørd4", "Nørd5", "Nørd6", "Nørd7", "Nørd8", "Nørd9", "Nørd10" };
        private string[] races = { "Dwarf", "Elf", "Gnome", "Goblin", "Orc", "Troll", "Vampire", "Werewolf", "Wizard", "Zombie" };


        internal void InitEnemyStats(int health, int damage, int name, int race)
        {
            Health = health;
            Damage = damage;
            Name = names[name];
            Race = races[race];
        }

    }
}