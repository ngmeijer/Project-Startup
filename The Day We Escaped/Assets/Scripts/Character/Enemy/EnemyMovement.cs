using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private EnemyAlertLevel alertLevel;
    private NavMeshAgent agent;
    private Transform currentTarget;
    private bool arrivedAtTarget;
    private float timer;

    [SerializeField] [Range(0, 30)] private float delayBeforeMoving;
    [SerializeField] [Range(0, 30)] private float _walkRange = 10f;
    [SerializeField] [Range(0, 20)] private float minDistanceToNewPoint = 5f;

    private List<Transform> _patrolPoints = new List<Transform>();
    [SerializeField] private GameObject _patrolPointsParent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            this.gameObject.AddComponent<NavMeshAgent>();
            agent = GetComponent<NavMeshAgent>();
        }

        findPossibleWaypoints();

        setNewDestination();
    }

    private void Update()
    {
        //Choose random point from list of patrolPoints. Then based on which one was chosen, choose a new one after arriving on point, or after certain duration.

        if (checkIfArrived() && delayBeforeMoving != 0)
        {
            Debug.Log("Arrived at target");
            timer += Time.deltaTime;
        }
        else
        {
            Debug.Log("Not yet at target");
        }

        if (timer >= delayBeforeMoving && checkIfArrived())
        {
            setNewDestination();
            timer = 0;
        }

        Debug.DrawLine(transform.position, currentTarget.position);
    }

    private void findPossibleWaypoints()
    {
        int amountOfWaypoints = _patrolPointsParent.transform.childCount;

        for (int index = 0; index < amountOfWaypoints; index++)
        {
            Transform child = _patrolPointsParent.transform.GetChild(index);
            _patrolPoints.Add(child);
        }
    }

    private void setNewDestination()
    {
        arrivedAtTarget = false;

        Transform newPoint = findNewPoint();

        agent.SetDestination(newPoint.position);
    }

    private Transform findNewPoint()
    {
        int index = Random.Range(0, _patrolPoints.Count);

        Transform newTarget = _patrolPoints[index];
        currentTarget = newTarget;

        return newTarget;
    }

    private bool checkIfArrived()
    {
        bool arrived = false;

        float distance = Vector3.Distance(transform.position, currentTarget.position);
        Debug.Log(distance);
        if (distance < 0.5f)
        {
            arrived = true;
        }

        return arrived;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minDistanceToNewPoint);
    }
}
