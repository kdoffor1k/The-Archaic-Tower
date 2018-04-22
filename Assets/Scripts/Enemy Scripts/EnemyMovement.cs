using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    public UnityEngine.AI.NavMeshAgent nav;
    public float range = 5f;
    Animator anim;


    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Destination").transform;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Vector3.Distance(player.position, transform.position) <= range)
        {
            anim.SetTrigger("AttackAnim");
        }
        else
        {
            nav.SetDestination(player.position);
        }
    }
}
