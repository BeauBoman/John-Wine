using UnityEngine;

[CreateAssetMenu(fileName = "Rotate Mover", menuName = "Components/Simulation/Mover/Rotate Lerp Mover", order = 1)]
public class RotateLerpMover : MoverSO
{
    public override void Move(Unit unit, Vector3 moveDir, float dt)
    {
        MovementStats stats = unit.Stats.GetStats(this);
        var unitSM = unit.State.MoveState;

        unitSM.Yaw += moveDir.x * stats.MaxSpeed;
        unitSM.Pitch -= moveDir.y * stats.MaxSpeed;
        unitSM.Pitch = Mathf.Clamp(unitSM.Pitch, -85f, 85f);

        float currentYawVelocity = unitSM.RotationalVelocity.x;
        float currentPitchVelocity = unitSM.RotationalVelocity.y;

        float smoothTime = Mathf.Max(0.001f, 1f / stats.Acceleration);

        float currentYaw = Mathf.SmoothDampAngle(unit.transform.localEulerAngles.y, unitSM.Yaw, ref currentYawVelocity, smoothTime, Mathf.Infinity, dt);
        float currentPitch = Mathf.SmoothDampAngle(unit.transform.localEulerAngles.x, unitSM.Pitch, ref currentPitchVelocity, smoothTime, Mathf.Infinity, dt);
        unitSM.RotationalVelocity = new Vector2(currentYawVelocity, currentPitchVelocity);
        unit.transform.localRotation = Quaternion.Euler(currentPitch, currentYaw, 0f);
    }
}
