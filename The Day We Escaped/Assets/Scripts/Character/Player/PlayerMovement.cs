using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterNS
{
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody rb;
        [Range(0.5f, 10)] [SerializeField] private float _moveSpeed = 3f;
        [Range(0.5f, 10)] [SerializeField] private float _jumpForce = 5f;

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
            //Raw to disable the smoothing, that would cause the character to keep moving a few frames.
            float horizontalAxis = Input.GetAxisRaw("Horizontal");
            float verticalAxis = Input.GetAxisRaw("Vertical");

            Vector3 moveVector = new Vector3(horizontalAxis, 0f, verticalAxis);

            moveVector = moveVector.normalized * _moveSpeed * Time.deltaTime;

            rb.MovePosition(transform.position + moveVector);
        }

        private void checkJump()
        {
            //rb.AddForce(new Vector3(0f, _jumpForce, 0f) * Time.deltaTime);
        }

        private void checkCrouch()
        {

        }
    }
}