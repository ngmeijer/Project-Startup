using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))]
public class FOVEditor : Editor
{
    private FieldOfView fov;

    private void OnSceneGUI()
    { 
        fov = (FieldOfView)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov._viewZRange);

        Vector3 viewAngleLeft = fov.DirectionFromAngle(-fov._viewAngle / 2, false);
        Vector3 viewAngleRight = fov.DirectionFromAngle(fov._viewAngle / 2, false);

        viewAngleLeft.y -= 1.00f;

        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleLeft * fov._viewZRange);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleRight * fov._viewZRange);
    }
}
