using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVAdvanced : MonoBehaviour
{
    private enum FOVDirection
    {
        Horizontal,
        Vertical,
    };

    [SerializeField] private FOVDirection FOVDir;

    [SerializeField] [Range(1, 50)] private int _amountOfRaysHorizontal;
    [SerializeField] [Range(2, 25)] private int _amountOfRaysVertical;

    [SerializeField] private float _viewZRange;
    [SerializeField] private float _maxHeight;

    [SerializeField] private Mesh mesh;

    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstacleMask;

    [SerializeField] private Camera cam;

    public float distanceBetweenRaysX;
    public float distanceBetweenRaysY;

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;

        cam.farClipPlane = _viewZRange;

        float frustumHeight = 1.0f * _viewZRange * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float frustumWidth = frustumHeight * cam.aspect;

        Gizmos.color = Color.red;

        Debug.Log(frustumWidth / _amountOfRaysHorizontal);

        distanceBetweenRaysX = (frustumWidth * 2) / _amountOfRaysHorizontal;
        distanceBetweenRaysY = (frustumHeight * 2) / _amountOfRaysVertical;

        float oldPosX = -frustumWidth;
        float oldPosY = frustumHeight;

        Gizmos.DrawLine(Vector3.zero, new Vector3(-frustumWidth, frustumHeight, _viewZRange));
        Gizmos.DrawLine(Vector3.zero, new Vector3(frustumWidth, frustumHeight, _viewZRange));

        for (int x = 0; x < _amountOfRaysHorizontal; x++)
        {
            oldPosX += distanceBetweenRaysX;
            oldPosY -= distanceBetweenRaysY;
            Gizmos.DrawLine(Vector3.zero, new Vector3(oldPosX, frustumHeight, _viewZRange));
        }
    }
}
