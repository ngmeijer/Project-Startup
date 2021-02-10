using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    private float minRange = 0.01f;

    //[Header("FOV")]
    [Range(0, 20)] [SerializeField] private float _viewZRange = 5f;
    ////[Range(0, 5)] [SerializeField] private float ViewX = 1f;
    //[Range(0, 100)] [SerializeField] private float _fovRadius = 85f;

    [SerializeField] private Mesh mesh;
    [SerializeField] private float _viewAngle = 50f;

    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;

    private List<Transform> visibleTargetsList = new List<Transform>();

    private void findVisibleTargets()
    {
        visibleTargetsList.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(Vector3.zero, _viewZRange, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < _viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    visibleTargetsList.Add(target);
                }
            }
        }
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
        drawAngleIndicator(Color.red);
    }

    private void drawRangeIndicator(Color gizmoColor)
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(Vector3.zero, _viewZRange);
    }

    private void drawAngleIndicator(Color gizmoColor)
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawLine(Vector3.zero, DirectionFromAngle(-_viewAngle / 2, false) * _viewZRange);
        Gizmos.DrawLine(Vector3.zero, DirectionFromAngle(_viewAngle / 2, false) * _viewZRange);
    }

    #region meh.

    //[SerializeField] [Range(2, 50)] private int _amountOfRaysHorizontal;
    //[SerializeField] [Range(2, 25)] private int _amountOfRaysVertical;

    //[SerializeField] private List<Ray> horizontalRays = new List<Ray>();

    //[SerializeField] private Mesh mesh;

    //[SerializeField] private LayerMask _targetMask;
    //[SerializeField] private LayerMask _obstacleMask;

    //[SerializeField] private Camera cam;

    //public float distanceBetweenRays;
    //private float oldPosX ;
    //private float oldPosY;
    //private List<Vector3> horOffset = new List<Vector3>();
    //private List<Vector3> verOffset = new List<Vector3>();

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
    //        oldPosX += 0.1f;
    //    }

    //    for (int verVecs = 0; verVecs < _amountOfRaysVertical; verVecs++)
    //    {
    //        Vector3 vertVecOffset = new Vector3(0f, oldPosY, _viewZRange);
    //        verOffset.Add(vertVecOffset);
    //        oldPosY += 0.2f;
    //    }

    //    distanceBetweenRays = _amountOfRaysHorizontal / cam.scaledPixelWidth;
    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.matrix = transform.localToWorldMatrix;

    //    Gizmos.color = Color.red;

    //    for (int horIndex = 0; horIndex < horOffset.Count; horIndex++)
    //    {
    //        for (int verIndex = 0; verIndex < verOffset.Count; verIndex++)
    //        {
    //            Gizmos.DrawRay(Vector3.zero, Vector3.forward + horOffset[horIndex]);
    //            Gizmos.DrawRay(Vector3.zero, Vector3.forward + verOffset[verIndex]);
    //        }
    //    }

    //    drawFrustum(Color.cyan);
    //}

    //private void drawFrustum(Color gizmoColor)
    //{
    //    //Gizmos.color = gizmoColor; 
    //    //Gizmos.DrawFrustum(Vector3.zero, fovRadius, ViewZRange, minRange, ViewX);
    //    cam.fieldOfView = _fovRadius;
    //    cam.farClipPlane = _viewZRange;

    //    for (int horizontal = 0; horizontal < _amountOfRaysHorizontal; horizontal++)
    //    {
    //        for (int vertical = 0; vertical < _amountOfRaysVertical; vertical++)
    //        {

    //        }
    //        //var worldSpaceCorner = cam.transform.TransformVector(frustumCorners[horizontal]);
    //        //Debug.DrawRay(cam.transform.position, worldSpaceCorner, Color.blue);
    //    }

    //    for (int y = 0; y < _amountOfRaysHorizontal; y++)
    //    {
    //        distanceBetweenRays = cam.fieldOfView / _amountOfRaysHorizontal;

    //        //Debug.DrawRay(cam.transform.position, new Vector3(-500 + 30, 0, cam.pixelWidth), Color.red);

    //        for (int x = 0; x < _amountOfRaysVertical; x++)
    //        {

    //        }
    //    }

    //    //Gizmos.color = Color.magenta;
    //    //foreach (Ray ray in horizontalRays)
    //    //{
    //    //    Gizmos.DrawRay(Vector3.zero, Vector3.forward * 5);
    //    //}
    //}

    #endregion
}