using UnityEngine;

public interface IPlayerMovement
{
    bool Move(Vector3 direction);
    bool Rotate(float yaw);
    Vector3 Velocity { get; }
    Vector3 Direction { get; }
}