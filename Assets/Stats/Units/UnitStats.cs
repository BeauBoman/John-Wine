using UnityEngine;

[CreateAssetMenu(fileName = "UnitStats", menuName = "Stats/UnitStats")]
public class UnitStats : ScriptableObject
{
    [Header("General")]
    [SerializeReference] public Mover Mover;
}
