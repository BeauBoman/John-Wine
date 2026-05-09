using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "Components/Units/Unit")]
public class UnitStats : ScriptableObject
{
    [Header("General")]
    [SerializeReference] public Mover Mover;
    [SerializeReference] public Effect Effect;
    [SerializeReference] public Behaviour Behaviour;
    [SerializeReference] public Actor Actor;
    [SerializeReference] public Weapon Weapon;
    [SerializeReference] public Tags Tags;
}
