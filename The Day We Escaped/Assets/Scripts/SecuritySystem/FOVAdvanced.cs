using System;
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

    private int usedAmountOfRays;

    [SerializeField] [Range(1, 20)] private float _viewZRange;
    [SerializeField] [Range(1, 100)] private float _fov;

    [SerializeField] [Range(-0.05f, 0.05f)] private float _scanSpeed;

    [SerializeField] private Mesh mesh;

    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstacleMask;

    [SerializeField] private Camera cam;

    private float distanceBetweenRaysX;
    private float distanceBetweenRaysY;

    private float oldPosX;
    private float oldPosY;

    private Rect frustumSize;
    private List<Vector3> directionsList = new List<Vector3>();

    private void Start()
    {
        cam.farClipPlane = _viewZRange;
        cam.fieldOfView = _fov;

        float frustumHeight = 1.0f * _viewZRange * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float frustumWidth = frustumHeight * cam.aspect;

        //Y-axis, TOP & BOTTOM
        frustumSize.yMin = frustumHeight;
        frustumSize.yMax = -frustumHeight;

        //X-axis LEFT & RIGHT
        frustumSize.xMin = -frustumWidth;
        frustumSize.xMax = frustumWidth;

        switch (FOVDir)
        {
            case FOVDirection.Horizontal:
                oldPosX = -frustumWidth;
                distanceBetweenRaysX = (frustumWidth * 2) / _amountOfRaysHorizontal;

                for (int x = 0; x < _amountOfRaysHorizontal; x++)
                {
                    Vector3 directionVec = new Vector3(oldPosX, oldPosY, _viewZRange);
                }
                break;
            case FOVDirection.Vertical:
                break;
        }
    }

    //Only for testing & scene view. 
    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;

        cam.farClipPlane = _viewZRange;
        cam.fieldOfView = _fov;

        float frustumHeight = 1.0f * _viewZRange * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float frustumWidth = frustumHeight * cam.aspect;

        //Y-axis, TOP & BOTTOM
        frustumSize.yMin = frustumHeight;
        frustumSize.yMax = -frustumHeight;

        //X-axis LEFT & RIGHT
        frustumSize.xMin = -frustumWidth;
        frustumSize.xMax = frustumWidth;

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

                if (oldPosY <= frustumSize.yMax || oldPosY >= frustumSize.yMin)
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
                    Vector3 direction = new Vector3(oldPosY, oldPosY, _viewZRange);
                    Gizmos.DrawLine(Vector3.zero, direction);
                    directionsList.Add(direction);
                }


                if (oldPosX <= frustumSize.xMin || oldPosX >= frustumSize.xMax)
                    _scanSpeed *= -1;

                Gizmos.DrawLine(Vector3.zero, new Vector3(oldPosX, frustumHeight * 2, _viewZRange));
                Gizmos.DrawLine(Vector3.zero, new Vector3(oldPosX, -frustumHeight * 2, _viewZRange));

                break;
        }

    }

    private void Update()
    {
        findVisibleTargets();
    }

    private void findVisibleTargets()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.yellow);
        Debug.Log(directionsList.Count);
        foreach (Vector3 directionVec in directionsList)
        {
            RaycastHit hit;
            Ray directionRay = new Ray(transform.position, directionVec);
            if (Physics.Raycast(directionRay, out hit, _viewZRange))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("hit player");
                }
            }
        }
    }
}
