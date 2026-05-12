using UnityEngine;

[CreateAssetMenu(fileName ="Default Weapon", menuName ="Components/Containers/Weapon/Default Weapon")]
public class DefaultWeapon : Weapon
{
    public override void Fire(PositionArgs positionArgs)
    {
        Unit spawnedUnit = LaunchComponents.UnitSpawner.Spawn(positionArgs);
    }
    public override void OnHit()
    {
        
    }
}
