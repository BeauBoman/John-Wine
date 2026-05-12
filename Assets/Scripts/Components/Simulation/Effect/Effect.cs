using UnityEngine;

public abstract class Effect : ScriptableObject
{
    /// <summary>
    /// Returns affected unit.
    /// </summary>
    /// <param name="targetUnit"></param>
    /// <returns></returns>
    [SerializeField] protected float _multiplier = 1;
    [SerializeField] protected float _amount;
    protected float _totalAmount => _amount * _multiplier;
    public abstract Unit Execute(Unit targetUnit);
    public void ModifyMultiplier(float amount)
    {
        _multiplier += amount;
    }
}
