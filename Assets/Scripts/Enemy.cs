using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Transform player;
    Animator animator;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        
        if(!navMeshAgent.isStopped)
        {
            if (Vector3.Distance(this.transform.position, player.position) < navMeshAgent.stoppingDistance + 0.1f)
            {
                navMeshAgent.isStopped = true;
                StartCoroutine("Attack");
                animator.SetBool("isWalk", false);
            }
            else
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.destination = player.position;
                animator.SetBool("isWalk", true);
            }
        }

        this.transform.LookAt(player.position);

    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("isAttack");
        yield return new WaitForSeconds(0.5f);

        if (Vector3.Distance(this.transform.position,player.position) < navMeshAgent.stoppingDistance + 0.1f)
        {
            StartCoroutine("Attack");
        }
        else
        {
            navMeshAgent.isStopped = false;
        }
    }

}
