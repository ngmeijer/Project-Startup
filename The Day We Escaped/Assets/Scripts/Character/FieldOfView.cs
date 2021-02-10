using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [Range(0, 10)] public float ViewZRange = 20f;
    [Range(0, 5)] public float ViewX = 1f;

    private float minRange = 0.01f;

    [SerializeField] private Mesh mesh;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float sixthSenseStrength = 3f;

    private void Start()
    {

    }

    public Vector3 DirectionFromAngle(float angleInDegs, bool isGlobal)
    {
        if (!isGlobal)
            angleInDegs += transform.localEulerAngles.y;

        return new Vector3(Mathf.Sin(Mathf.Deg2Rad * angleInDegs), 0, Mathf.Cos(Mathf.Deg2Rad * angleInDegs));
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        drawRangeIndicator(Color.yellow);
        drawFrustum(Color.cyan);
    }

    private void drawRangeIndicator(Color gizmoColor)
    {
        Gizmos.color = gizmoColor;
        //Gizmos.DrawWireSphere(Vector3.zero, MaxViewRange);
        Gizmos.DrawWireMesh(mesh, Vector3.zero + offset, Quaternion.identity, new Vector3(ViewX * 10, 2, ViewZRange));
    }

    private void drawFrustum(Color gizmoColor)
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawFrustum(Vector3.zero, 85f, ViewZRange / 2, minRange, ViewX);
    }
}