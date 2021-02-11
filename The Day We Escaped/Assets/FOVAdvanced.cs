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

    private float oldPosX;
    private float oldPosY;

    private float frustumBottom;
    private float frustumTop;
    private float frustumLeft;
    private float frustumRight;

    [SerializeField] private float _scanSpeed = 0.01f;

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;

        cam.farClipPlane = _viewZRange;

        float frustumHeight = 1.0f * _viewZRange * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float frustumWidth = frustumHeight * cam.aspect;

        frustumTop = frustumHeight;
        frustumBottom = -frustumHeight;
        frustumLeft = -frustumWidth;
        frustumRight = frustumWidth;

        Gizmos.color = Color.red;

        switch (FOVDir)
        {
            case FOVDirection.Horizontal:
                oldPosX = -frustumWidth;
                distanceBetweenRaysX = (frustumWidth * 2) / _amountOfRaysHorizontal;

                oldPosY -= _scanSpeed;

                for (int x = 0; x < _amountOfRaysHorizontal; x++)
                {
                    oldPosX += distanceBetweenRaysX;
                    Gizmos.DrawLine(Vector3.zero, new Vector3(oldPosX, oldPosY, _viewZRange));
                }

                if (oldPosY <= frustumBottom || oldPosY >= frustumTop)
                    _scanSpeed *= -1;

                Gizmos.DrawLine(Vector3.zero, new Vector3(-frustumWidth, oldPosY, _viewZRange));
                Gizmos.DrawLine(Vector3.zero, new Vector3(frustumWidth, oldPosY, _viewZRange));
                break;

            case FOVDirection.Vertical:
                oldPosY = -frustumHeight * 2;

                distanceBetweenRaysY = (frustumHeight * 4) / _amountOfRaysVertical;

                oldPosX -= _scanSpeed;

                for (int y = 0; y < _amountOfRaysVertical; y++)
                {
                    oldPosY += distanceBetweenRaysY;
                    Gizmos.DrawLine(Vector3.zero, new Vector3(oldPosX, oldPosY, _viewZRange));
                }


                if (oldPosX <= frustumLeft || oldPosX >= frustumRight)
                    _scanSpeed *= -1;

                Gizmos.DrawLine(Vector3.zero, new Vector3(oldPosX, frustumHeight * 2, _viewZRange));
                Gizmos.DrawLine(Vector3.zero, new Vector3(oldPosX, -frustumHeight * 2, _viewZRange));

                break;
        }

    }
}
