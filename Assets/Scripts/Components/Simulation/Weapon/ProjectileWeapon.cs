using UnityEngine;

[CreateAssetMenu(fileName ="Default Weapon", menuName ="Components/Components/Weapon/Default Weapon")]
public class ProjectileWeapon : Weapon
{
    public override void Fire(PositionArgs positionArgs, Unit owner)
    {
        Unit spawnedUnit = LaunchComponents.UnitSpawner.Spawn(positionArgs, owner);
    }
    public override void OnHit()
    {
        
    }
}
