using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Item Item;
    NavMeshAgent navMeshAgent;
    public Transform player;
    Animator animator;
    public bool isAttackCheck = false;
    public int hp = 3;
    Renderer[] renderers;
    Color origniColor;
    public bool isStop = false;

    public float navDistance = 15f; // 적이 나를 따라올 최소 위치
    public float SpwanTimePossible = 20f; // 이건 안씀, 원래는 아이템 생성 확률인데 안넣었음
    public GameObject item;

    private void Awake()
    {
        Item = GetComponent<Item>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        navMeshAgent.destination = player.position;
        renderers = this.GetComponentsInChildren<Renderer>();
        origniColor = renderers[0].material.color;

        item = Item.SetItem(item);
    }

    void Update()
    {

        if (isStop || Vector3.Distance(this.transform.position, player.position) > navDistance)
        {
            navMeshAgent.isStopped = true;
            return;
        }
        if (Vector3.Distance(this.transform.position, player.position) < navMeshAgent.stoppingDistance + 0.1f)
        {
            navMeshAgent.isStopped = true;
            StartCoroutine("Attack");
            animator.SetBool("isWalk", false);
        }
        else
        {
            navMeshAgent.isStopped = false;
            animator.SetBool("isWalk", true);
            navMeshAgent.destination = player.position;
        }

        this.transform.LookAt(player.position);
    }

    IEnumerator Attack()
    {
        if (!isStop)
        {
            yield return new WaitForSeconds(0.5f);
            isAttackCheck = true;
            animator.SetTrigger("isAttack");

            yield return new WaitForSeconds(0.5f);
            isAttackCheck = false;
            if (Vector3.Distance(this.transform.position, player.position) < navMeshAgent.stoppingDistance + 0.1f)
            {
                StartCoroutine("Attack");
            }
            else
            {
                navMeshAgent.isStopped = false;
            }
        }
    }

    public void SetHp(int damage)
    {
        if (!isStop)
        {
            hp -= damage;
            if (hp <= 0)
            {
               isStop = true;
                hp = 0;
                Debug.Log("die");
                animator.SetTrigger("Death");
                Instantiate(item, gameObject.transform.position + new Vector3(0, 1, 0), gameObject.transform.rotation);
                
                navMeshAgent.isStopped = true;

            }
            else
            {
                StartCoroutine("HitColor");
            }
        }
    }

    IEnumerator HitColor()
    {
        foreach (Renderer render in renderers)
        {
            render.material.color = Color.red;
        }
        yield return new WaitForSeconds(0.5f);
        foreach (Renderer render in renderers)
        {
            render.material.color = origniColor;
        }
    }
}
