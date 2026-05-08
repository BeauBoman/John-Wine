using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "Components/Units/Unit")]
public class UnitStats : ScriptableObject
{
    [Header("General")]
    [SerializeReference] public Mover Mover;
}
