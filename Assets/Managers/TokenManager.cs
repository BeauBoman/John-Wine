using System.Collections.Generic;
using UnityEngine;

public class TokenManager : MonoBehaviour
{
    public static TokenManager instance;

    [SerializeField] private int _maxTokens;
    private Dictionary<GameObject, float> _activeTokens = new();
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
            lowestPriorityEnemy.GetComponent<EnemyController>().ReleaseToken();
            _activeTokens.Add(enemy, priority);
            return true;
        }
        return false;
    }
    public void RemoveToken(GameObject enemy)
    {
        if (_activeTokens.ContainsKey(enemy))
        {
            _activeTokens.Remove(enemy);
        }
    }
}
