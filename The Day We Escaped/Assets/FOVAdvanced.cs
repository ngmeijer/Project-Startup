using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVAdvanced : MonoBehaviour
{
    [SerializeField] [Range(1, 50)] private int _amountOfRaysHorizontal;
    [SerializeField] [Range(2, 25)] private int _amountOfRaysVertical;

    [SerializeField] private List<Ray> horizontalRays = new List<Ray>();

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
    private List<Vector3> horOffset = new List<Vector3>();
    private List<Vector3> verOffset = new List<Vector3>();

    //private void Start()
    //{
    //    Vector3[] frustumCorners = new Vector3[4];
    //    cam.CalculateFrustumCorners(new Rect(0, 0, 1, 1), cam.farClipPlane, Camera.MonoOrStereoscopicEye.Mono, frustumCorners);

    //    oldPosX = frustumCorners[0].x;
    //    oldPosY = frustumCorners[0].y;
    //    for (int horVecs = 0; horVecs < _amountOfRaysHorizontal; horVecs++)
    //    {
    //        Vector3 horVecOffset = new Vector3(oldPosX, 0f, _viewZRange);
    //        horOffset.Add(horVecOffset);
    //        oldPosX += 1.0f;
    //    }

    //    for (int verVecs = 0; verVecs < _amountOfRaysVertical; verVecs++)
    //    {
    //        Vector3 vertVecOffset = new Vector3(0f, oldPosY, _viewZRange);
    //        verOffset.Add(vertVecOffset);
    //        oldPosY += 0.2f;
    //    }

    //    distanceBetweenRays = _amountOfRaysHorizontal / cam.scaledPixelWidth;
    //}

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

        for (int y = 0; y < _amountOfRaysVertical; y++)
        {
            
        }

        Gizmos.DrawLine(Vector3.zero, new Vector3(-frustumWidth, frustumHeight, _viewZRange));
        Gizmos.DrawLine(Vector3.zero, new Vector3(frustumWidth, frustumHeight, _viewZRange));

        for (int x = 0; x < _amountOfRaysHorizontal; x++)
        {
            oldPosX += distanceBetweenRaysX;
            oldPosY -= distanceBetweenRaysY;
            Gizmos.DrawLine(Vector3.zero, new Vector3(oldPosX, frustumHeight, _viewZRange));
        }

        //Gizmos.DrawLine(Vector3.zero, new Vector3(-3, 0, _viewZRange));
        //Gizmos.DrawLine(Vector3.zero, new Vector3(-0, 0, _viewZRange));
        //Gizmos.DrawLine(Vector3.zero, new Vector3(3, 0, _viewZRange));

        //Gizmos.DrawLine(Vector3.zero, new Vector3(-3, -2, _viewZRange));
        //Gizmos.DrawLine(Vector3.zero, new Vector3(-0, -2, _viewZRange));
        //Gizmos.DrawLine(Vector3.zero, new Vector3(3, -2, _viewZRange));


        //for (int horIndex = 0; horIndex < horOffset.Count; horIndex++)
        //{
        //    for (int verIndex = 0; verIndex < verOffset.Count; verIndex++)
        //    {
        //        Gizmos.DrawLine(Vector3.zero, horOffset[horIndex]);
        //        Gizmos.DrawLine(Vector3.zero, verOffset[verIndex]);
        //    }
        //}
    }

    private void drawFrustum(Color gizmoColor)
    {
        ////Gizmos.color = gizmoColor; 
        ////Gizmos.DrawFrustum(Vector3.zero, fovRadius, ViewZRange, minRange, ViewX);
        ////cam.fieldOfView = _fovRadius;
        ////cam.farClipPlane = _viewZRange;

        //for (int horizontal = 0; horizontal < _amountOfRaysHorizontal; horizontal++)
        //{
        //    for (int vertical = 0; vertical < _amountOfRaysVertical; vertical++)
        //    {

        //    }
        //    //var worldSpaceCorner = cam.transform.TransformVector(frustumCorners[horizontal]);
        //    //Debug.DrawRay(cam.transform.position, worldSpaceCorner, Color.blue);
        //}

        //for (int y = 0; y < _amountOfRaysHorizontal; y++)
        //{
        //    distanceBetweenRays = cam.fieldOfView / _amountOfRaysHorizontal;

        //    //Debug.DrawRay(cam.transform.position, new Vector3(-500 + 30, 0, cam.pixelWidth), Color.red);

        //    for (int x = 0; x < _amountOfRaysVertical; x++)
        //    {

        //    }
        //}

        ////Gizmos.color = Color.magenta;
        ////foreach (Ray ray in horizontalRays)
        ////{
        ////    Gizmos.DrawRay(Vector3.zero, Vector3.forward * 5);
        ////}
    }
}
