using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoMovement : MonoBehaviour {

    private Transform target;
    private int wavepointIndex = 0;
    Transform player;
    public UnityEngine.AI.NavMeshAgent nav;
    public float range = 5f;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Destination").transform;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = Waypoints.points[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        nav.SetDestination(target.position);
        
        if (Vector3.Distance(transform.position, target.position) <= 7f)
        {
            GetNextWaypoint();
        }

        
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            Restart();
            wavepointIndex = 0;
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    void Restart()
    {
        target = player;
    }
}
