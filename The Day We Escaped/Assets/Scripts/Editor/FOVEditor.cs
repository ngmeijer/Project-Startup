using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FOVEditor : MonoBehaviour
{
    private FieldOfView fov;

    private void Start()
    {
        fov = FindObjectOfType<FieldOfView>();
    }

    private void OnSceneGUI()
    {
        drawRangeIndicator(Color.yellow);
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = fov.transform.localToWorldMatrix;
        drawRangeIndicator(Color.yellow);
        drawAngleIndicator(Color.magenta);
    }

    private void drawRangeIndicator(Color handleColor)
    {
        Handles.color = handleColor;
        Handles.DrawWireArc(Vector3.zero, fov.transform.up, fov.transform.forward, 360, fov.ViewRange);
    }

    private void drawAngleIndicator(Color handleColor)
    {
        Gizmos.color = handleColor;
        Vector3 leftViewAngle = fov.DirectionFromAngle(-fov.ViewWidthHeight / 2, false);
        Vector3 rightViewAngle = fov.DirectionFromAngle(fov.ViewWidthHeight / 2, false);

        Gizmos.DrawLine(Vector3.zero, fov.transform.localPosition + leftViewAngle * fov.ViewRange);
        Gizmos.DrawLine(Vector3.zero, fov.transform.localPosition + rightViewAngle * fov.ViewRange);
    }
}