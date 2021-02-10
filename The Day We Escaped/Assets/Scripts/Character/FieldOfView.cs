using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [Range(0, 360)] public float ViewWidthHeight = 250f;
    [Range(0, 10)] public float ViewRange = 20f;
    [Range(0, 5)] public float ViewWidth = 1f;

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
        Gizmos.DrawWireSphere(Vector3.zero, ViewRange);
    }

    private void drawFrustum(Color gizmoColor)
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawFrustum(Vector3.zero, ViewWidthHeight, ViewRange, 0.1f, ViewWidth);
    }

    private void drawAngleIndicator(Color gizmoColor)
    {
        Gizmos.color = gizmoColor;
        Vector3 leftViewAngle = DirectionFromAngle(-ViewWidthHeight / 2, false);
        Vector3 rightViewAngle = DirectionFromAngle(ViewWidthHeight / 2, false);

        Gizmos.DrawLine(Vector3.zero, leftViewAngle * ViewRange);
        Gizmos.DrawLine(Vector3.zero, rightViewAngle * ViewRange);
    }
}