using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viewcone : MonoBehaviour
{
    [SerializeField] private float _viewAngle = 20f;

    public Transform target;

    private void Update()
    {
        Vector3 targetDirection = target.position - transform.position;

        float angle = Vector3.Angle(targetDirection, transform.forward);

        if (angle < _viewAngle)
        {
            Debug.Log("Saw player");
        }
    }
}