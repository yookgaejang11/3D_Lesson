using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Transform player;
    Animator animator;
    public bool isAttackCheck = false;
    int hp = 2;
    bool isStop = false;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        navMeshAgent.destination = player.position;
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
        isAttackCheck = true;
        yield return new WaitForSeconds(0.5f);
        isAttackCheck = false;
        if (Vector3.Distance(this.transform.position,player.position) < navMeshAgent.stoppingDistance + 0.1f)
        {
            StartCoroutine("Attack");
        }
        else
        {
            navMeshAgent.isStopped = false;
        }
    }

    public void SetHp(int damage)
    {
        if(!isStop)
        {
            hp -=damage;
            if (hp <= 0)
            {
                hp = 0;
                Debug.Log("die");
                animator.SetTrigger("Death");
                isStop = true;
            }
        }
    }

}
