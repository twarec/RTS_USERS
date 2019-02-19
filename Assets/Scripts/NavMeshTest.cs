using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using YG_EventSystem;

public class NavMeshTest : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private float time;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        GameEvent.AddEvent(UnitsUpdate, Method.Update);
    }

    private void UnitsUpdate(float t)
    {
        if (agent.remainingDistance > 0)
        {
            if(Vector3.Angle(transform.forward, transform.position - agent.nextPosition) > 5)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.position - agent.nextPosition), 15);
                agent.velocity = Vector3.zero;
                agent.updateUpAxis = true;
            }
            else
            {
                agent.updateUpAxis = false;
            }
        }
    }
}
