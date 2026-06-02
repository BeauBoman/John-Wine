using System.Collections.Generic;
using UnityEngine;

public class TokenManager : MonoBehaviour
{
    public static TokenManager instance;
    

    [SerializeField] private int _maxTokens;
    [HideInInspector] public Dictionary<GameObject, float> _activeTokens = new();
    private Dictionary <GameObject, EnemyPathfinding> _enemies = new();
    void Awake()
    {
        instance = this;
    }
    public bool RequestToken(GameObject enemy, float priority)
    {
        priority = priority * -1f; //inverting priority

        if (_activeTokens.ContainsKey(enemy))
        {
            _activeTokens[enemy] = priority;
            return true;
        }
        else
        {
            _enemies.Add(enemy, enemy.GetComponent<EnemyPathfinding>());
        }
        if (_activeTokens.Count < _maxTokens)
        {
            _activeTokens.Add(enemy, priority);
            return true;
        }

        GameObject lowestPriorityEnemy = null;
        float lowestPriority = float.MaxValue;

        foreach (var token in _activeTokens)
        {
            if (token.Value < lowestPriority)
            {
                lowestPriority = token.Value;
                lowestPriorityEnemy = token.Key;
            }
        }

        if (priority > lowestPriority)
        {
            _enemies.TryGetValue(lowestPriorityEnemy, out EnemyPathfinding enemyPathfinding);

            enemyPathfinding.ReleaseToken();
            _activeTokens.Add(enemy, priority);
            return true;
        }
        return false;
    }
}
