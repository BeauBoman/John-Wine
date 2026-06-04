using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Stats/Pathfinding", order = 1)]
public class PathfindingStats : ScriptableObject
{
    public float maxDistanceSearch;
    public float minDistanceFlank;
    public float maxDistanceFlank;

    [Header("Priority is negative")]
    public float tokenPriority;
}
