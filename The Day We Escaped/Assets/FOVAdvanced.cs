using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVAdvanced : MonoBehaviour
{ private enum FOVdirection
    {
        Horizontal,
        Vertical,
    };

    [SerializeField] private FOVdirection fovDir;

    [SerializeField] [Range(1, 50)] private int _amountOfRaysHorizontal;
    [SerializeField] [Range(2, 25)] private int _amountOfRaysVertical;

    [SerializeField] private List<Ray> horizontalRays = new List<Ray>();

    [SerializeField] private float _viewZRange;

    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstacleMask;

    [SerializeField] private Camera cam;

    public float distanceBetweenRaysX;
    public float distanceBetweenRaysY;

    private float oldPosX;
    private float oldPosY;

    //Vertical
    private float frustumHeight;
    private float frustumBottom;
    private float frustumWidth;
    private float originalFrustumHeight;

    //Horizontal
    private float frustumLeft;
    private float originalFrustumWidth;

    [SerializeField] private float scanSpeed = 0.01f;

    private void Awake()
    {
        oldPosY = frustumHeight;
        oldPosX = frustumWidth;
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        cam.farClipPlane = _viewZRange;
        Gizmos.color = Color.red;

        frustumHeight = 1f * _viewZRange * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        frustumWidth = frustumHeight * cam.aspect;


        frustumBottom = -frustumHeight;
        originalFrustumHeight = frustumHeight;

        frustumLeft = -frustumWidth;
        originalFrustumWidth = frustumWidth;

        distanceBetweenRaysX = (frustumWidth * 2) / _amountOfRaysHorizontal;
        distanceBetweenRaysY = (frustumHeight * 2) / _amountOfRaysVertical;

        oldPosX = -frustumWidth;

        switch (fovDir)
        {
            case FOVdirection.Horizontal:
                for (int x = 0; x < _amountOfRaysVertical; x++)
                {
                    oldPosX += distanceBetweenRaysX;

                    Gizmos.DrawLine(Vector3.zero, new Vector3(oldPosX, oldPosY, _viewZRange));
                }

                oldPosY -= scanSpeed;

                if (oldPosY <= frustumBottom || oldPosY >= originalFrustumHeight)
                    scanSpeed *= -1;

                //Mandatory ray left
                Gizmos.DrawLine(Vector3.zero, new Vector3(-frustumWidth, oldPosY, _viewZRange));

                //Mandatory ray right
                Gizmos.DrawLine(Vector3.zero, new Vector3(frustumWidth, oldPosY, _viewZRange));
                break;
            case FOVdirection.Vertical:
                //for (int x = 0; x < _amountOfRaysHorizontal; x++)
                //{
                //    oldPosY += distanceBetweenRaysY;

                //    Gizmos.DrawLine(Vector3.zero, new Vector3(oldPosX, oldPosY, _viewZRange));
                //}

                oldPosX -= scanSpeed;

                if (oldPosX <= frustumLeft || oldPosX >= originalFrustumWidth)
                    scanSpeed *= -1;

                //Mandatory ray top
                Gizmos.DrawLine(Vector3.zero, new Vector3(oldPosX, frustumHeight, _viewZRange));

                //Mandatory ray menu
                Gizmos.DrawLine(Vector3.zero, new Vector3(oldPosX, -frustumHeight, _viewZRange));
                break;
        }

    }
}
