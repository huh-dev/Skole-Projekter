using Heltevagten.Enums;
using Heltevagten.Exceptions;
using Heltevagten.Incidents;

namespace Heltevagten;

/// <summary>
/// Abstract base for all heroes. Energy and availability are encapsulated
/// so callers cannot set an invalid energy value or force a busy hero into service.
/// </summary>
public abstract class Hero
{
    private const int MinEnergyLevel = 0;
    private const int MaxEnergyLevel = 150;

    private int _energyLevel;
    private readonly int _maxEnergy;

    public string Name { get; private set; }
    public int EnergyLevel => _energyLevel;
    public int MaxEnergy => _maxEnergy;
    public int Cost { get; private set; }
    public HeroState State { get; private set; }
    public Location CurrentLocation { get; protected set; }

    protected Hero(string name, int energyLevel, int cost, Location currentLocation)
    {
        if (energyLevel < MinEnergyLevel || energyLevel > MaxEnergyLevel)
        {
            throw new ArgumentOutOfRangeException(
                nameof(energyLevel),
                $"Energy must be between {MinEnergyLevel} and {MaxEnergyLevel}.");
        }

        if (cost < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(cost), "Cost cannot be negative.");
        }

        Name = name;
        _maxEnergy = energyLevel;
        _energyLevel = energyLevel;
        Cost = cost;
        CurrentLocation = currentLocation;
        State = HeroState.Available;
    }


    public abstract string UseSignatureMove();

    public bool HasEnoughEnergy(int amount)
    {
        return _energyLevel >= amount;
    }

    public void UseEnergy(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Energy cost cannot be negative.");
        }

        _energyLevel = Math.Max(MinEnergyLevel, _energyLevel - amount);
    }

    internal void Release()
    {
        State = _energyLevel <= MinEnergyLevel ? HeroState.Recharging : HeroState.Available;
    }

    internal void Recharge()
    {
        _energyLevel = _maxEnergy;
        if (State == HeroState.Recharging)
        {
            State = HeroState.Available;
        }
    }

    public virtual void Dispatch(Incident incident)
    {
        if (State != HeroState.Available)
        {
            throw new HeroUnavailableException($"{Name} is {State.ToString().ToLower()} and cannot be dispatched.");
        }

        State = HeroState.Dispatched;
        CurrentLocation = incident.Location;
    }
}
