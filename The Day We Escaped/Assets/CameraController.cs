using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector3 _eulerAngleVelocity = new Vector3(0, 100, 0);

    [SerializeField] private float _mouseSensitivityX = 90f;
    [SerializeField] private float _mouseSensitivityY = 90f;
    [SerializeField] private bool _isInverted = true;

    private void Start()
    {
        _rb = GetComponentInParent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float lookHoriz = Input.GetAxisRaw("Mouse Y") * _mouseSensitivityX * Time.deltaTime;
        float lookVert = Input.GetAxisRaw("Mouse X") * _mouseSensitivityY * Time.deltaTime;

        //Horizontal
        Quaternion deltaRot = Quaternion.Euler(new Vector3(0, lookVert, 0));
        _rb.MoveRotation(_rb.rotation * deltaRot);

        //Vertical 
        transform.Rotate(Vector3.right * lookHoriz * -1);
    }
}