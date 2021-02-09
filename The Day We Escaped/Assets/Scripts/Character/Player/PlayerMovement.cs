using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [Range(0.5f, 10)] [SerializeField] private float _moveSpeed = 3f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    private void FixedUpdate()
    {
        checkMovement();
        checkJump();
        checkCrouch();
    }

    private void checkMovement()
    {
        float horizontalAxis = Input.GetAxis("Horizontal") * _moveSpeed * Time.fixedDeltaTime;
        float verticalAxis = Input.GetAxis("Vertical") * _moveSpeed * Time.fixedDeltaTime;

        transform.position = new Vector3(transform.position.x + horizontalAxis, transform.position.y, transform.position.z + verticalAxis);

        rb.MovePosition(transform.position.normalized);
    }

    private void checkJump()
    {

    }

    private void checkCrouch()
    {

    }
}
