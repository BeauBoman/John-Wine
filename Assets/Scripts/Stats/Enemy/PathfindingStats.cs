using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Stats/Pathfinding", order = 1)]
public class PathfindingStats : ScriptableObject
{
    public float maxDistanceSearch;
    public float minDistanceFlank;
    public float maxDistanceFlank;
    public float stoppingDistance;

    public float tokenPriority;
}
