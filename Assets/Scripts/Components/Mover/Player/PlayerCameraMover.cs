using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCameraMover", menuName = "Components/Movers/PlayerCameraMover", order = 1)]
public class PlayerCameraMover : Mover
{
    public override void Move(Unit unit, Vector3 dir, float deltaTime)
    {
        var state = unit.State;
        var stats = unit.Stats.Movement;

        state.Pitch -= dir.y * stats.MaxSpeed;
        state.Pitch = Mathf.Clamp(state.Pitch, -90f, 90f);

        state.Yaw += dir.x * stats.MaxSpeed;

        unit.transform.localRotation = Quaternion.Euler(state.Pitch, state.Yaw, 0f);
    }
}
