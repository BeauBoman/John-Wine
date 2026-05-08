using System;

[Serializable]
public class Stats
{
    public MovementStats MovementBuffs;
    public MovementStats Movement { get { return originMovement + MovementBuffs; } }

    private MovementStats originMovement;
    public Stats(StatsTemplate template)
    {
        originMovement = template.Movement;
    }
}