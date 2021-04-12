using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public float Speed;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Speed;
    }

    public void To(Vector3 point)
    {
        if (agent != null && gameObject != null && agent.isActiveAndEnabled)
        {
            agent.SetDestination(point);
            var direction = (point + transform.position).normalized;
            transform.forward = direction;
        }
    }

}
