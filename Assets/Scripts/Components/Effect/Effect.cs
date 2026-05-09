using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public abstract Unit Execute(Unit unit);
}
