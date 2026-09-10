# Heltevagten

Console dispatch center for assigning superheroes to city incidents.

## Build and run

From this folder:

```bash
dotnet run
```

Requires the .NET SDK (the project targets `net10.0`).

## How to play

You have 7 days. Start with Falcon, Atlas, and Pulse.

1. **Dispatch Heroes** — pick incident(s), then a strategy (closest, first available, or manual).
2. **Hero Shop** — spend points to unlock more heroes.
3. **Complete responses** — heroes use their signature move, incidents are resolved, points are awarded.
0. **Quit**

A hero who hits 0 energy becomes `Recharging` and cannot be sent out until they rest (complete responses with no active assignments) or the next day starts.

## Loose coupling (requirement h)

`DispatchCenter` does not decide *how* a hero is chosen. It receives an `IDispatchStrategy` in the constructor and calls `SelectHero`. That is why first-available and closest-hero both work from the dispatch menu without changing `DispatchCenter`: only the strategy object is swapped. A new policy (strongest hero, flying heroes only, and so on) is another class that implements the same interface.
