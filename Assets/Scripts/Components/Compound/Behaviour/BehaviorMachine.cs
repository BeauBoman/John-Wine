using System.Collections.Generic;
using UnityEngine;
public class BehaviorMachine : IUpdatable
{
    private Unit _unit;
    private List<PeriodicBehavior> PeriodicBehaviors = new();
    private List<TemporaryBehavior> TemporaryBehaviors = new();
    public void ApplyBehavior(TemporaryBehavior b)
    {
        TemporaryBehaviors.Add(b);
        b.OnStart();
    }
    public void ApplyBehavior(PeriodicBehavior b)
    {
        PeriodicBehaviors.Add(b);
    }
    private void EndBehavior(TemporaryBehavior b)
    {
        TemporaryBehaviors.Remove(b);
    }
    private void EndBehavior(PeriodicBehavior b)
    {
        PeriodicBehaviors.Remove(b);
    }
    public void OnUpdate(float dt)
    {
        UpdateTemporaryBehaviors(dt);
        UpdatePeriodicBehaviors(dt);
    }
    private void UpdateTemporaryBehaviors(float dt)
    {
        for (int i = TemporaryBehaviors.Count - 1; i >= 0; i--)
        {
            TemporaryBehaviors[i].OnUpdate(dt);
            if (TemporaryBehaviors[i].Ended)
            {
                TemporaryBehaviors[i].OnEnd();
                EndBehavior(TemporaryBehaviors[i]);
            }
        }
    }
    private void UpdatePeriodicBehaviors(float dt)
    {
        for (int i = PeriodicBehaviors.Count - 1; i >= 0; i--)
        {
            PeriodicBehaviors[i].OnUpdate(dt);
            if (PeriodicBehaviors[i].PeriodTicked)
            {
                PeriodicBehaviors[i].OnPeriod();
                PeriodicBehaviors[i].ResetPeriodProgress();
            }
            if (PeriodicBehaviors[i].Ended)
            {
                EndBehavior(PeriodicBehaviors[i]);
            }
        }
    }
    public BehaviorMachine(Unit unit)
    {
        _unit = unit;
    }
    //public void RemoveBehaviorByType(BehaviorSO bSO)
    //{
    //    Behavior beh = bSO.CreateBehavior();
    //    for (int i = Behaviors.Count - 1; i >= 0; i--)
    //    {
    //        if (Behaviors[i].GetType() == beh.GetType())
    //            Behaviors.Remove(Behaviors[i]);
    //    }
    //}
}
