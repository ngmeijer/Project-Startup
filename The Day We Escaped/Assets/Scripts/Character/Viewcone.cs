using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viewcone : MonoBehaviour
{
    [SerializeField] private float _viewAngle = 20f;
    [SerializeField] private float _range = 20f;

    public Vector3 DirectionFromAngle(float angleInDegs)
    {
        return new Vector3(Mathf.Sin(Mathf.Deg2Rad * angleInDegs), Mathf.Cos(Mathf.Deg2Rad * angleInDegs));
    }
}