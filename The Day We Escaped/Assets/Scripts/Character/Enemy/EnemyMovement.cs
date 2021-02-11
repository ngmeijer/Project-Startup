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

    [SerializeField] private float _walkRange = 10f;
    [SerializeField] private List<Transform> _patrolPoints = new List<Transform>();

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            this.gameObject.AddComponent<NavMeshAgent>();
            agent = GetComponent<NavMeshAgent>();
        }

        setNewDestination();
    }

    private void Update()
    {
        //Choose random point from list of patrolPoints. Then based on which one was chosen, choose a new one after arriving on point, or after certain duration.

        if (arrivedAtTarget)
        {
            setNewDestination();
        }

        StartCoroutine(checkIfArrived());
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

    private IEnumerator checkIfArrived()
    {
        while (transform.position != currentTarget.position)
        {
            Debug.Log("not yet at target");
            yield return null;
        }

        Debug.Log("Arrived at target.");
        arrivedAtTarget = true;

        yield return null;
    }
}
