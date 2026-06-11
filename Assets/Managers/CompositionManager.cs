using System;
using System.Collections.Generic;
using UnityEngine;

public class CompositionManager : MonoBehaviour
{
    public static CompositionManager Instance;

    public TeamSetting[] TeamsSettings;

    private readonly Dictionary<(Team, Team), TeamsFilter> _relations = new Dictionary<(Team, Team), TeamsFilter>();

    private void Awake()
    {
        Instance = this;
        InitializeRelations();
    }

    private void InitializeRelations()
    {
        _relations.Clear();

        foreach (var setting in TeamsSettings)
        {
            foreach (Team target in Enum.GetValues(typeof(Team)))
            {
                TeamsFilter relation = TeamsFilter.Neutral;

                if (setting.sourceTeam == target)
                {
                    relation = TeamsFilter.Own;
                }
                else if (setting.allyTowards.HasFlag((TeamsFlags)(1 << (int)target)))
                {
                    relation = TeamsFilter.Allies;
                }
                else if (setting.enemyTowards.HasFlag((TeamsFlags)(1 << (int)target)))
                {
                    relation = TeamsFilter.Enemies;
                }

                _relations[(setting.sourceTeam, target)] = relation;
            }
        }
    }

    public TeamsFilter GetRelation(Team sourceTeam, Team targetTeam)
    {
        if (_relations.TryGetValue((sourceTeam, targetTeam), out var relation))
        {
            return relation;
        }
        return TeamsFilter.Neutral;
    }
}

[System.Serializable]
public struct TeamSetting
{
    public Team sourceTeam;
    public TeamsFlags allyTowards;
    public TeamsFlags enemyTowards;
}

public enum Team { Team0, Team1, Team2, Team3 }

[Flags]
public enum TeamsFlags
{
    None = 0,
    Team0 = 1 << 0,
    Team1 = 1 << 1,
    Team2 = 1 << 2, 
    Team3 = 1 << 3  
}
[Flags]
public enum TeamsFilter 
{ 
    None = 0,
    Neutral = 1 << 0, 
    Own = 1 << 1,
    Allies = 1 << 2,
    Enemies = 1 << 3,
}