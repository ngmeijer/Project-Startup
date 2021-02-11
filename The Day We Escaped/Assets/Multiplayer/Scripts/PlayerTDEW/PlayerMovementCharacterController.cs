using UnityEngine;

public class PlayerMovementCharacterController : MonoBehaviour, IPlayerMovement
{
    [SerializeField] private CharacterController _cc;

    [SerializeField] private float _speed;
    private Vector3 _direction;

    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
    }

    public bool Move(Vector3 direction)
    {
        _cc.SimpleMove(_speed * direction);

        return true;
    }

    public bool Rotate(float yaw)
    {
        transform.localRotation = Quaternion.Euler(0,yaw,0);
        return true;
    }

    public Vector3 Velocity => _cc.velocity;

    public Vector3 Direction => _direction;
}