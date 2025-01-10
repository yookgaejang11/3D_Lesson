using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController characterController;
    public float speed = 2.0f;
    Animator animator;
    bool isWalk = false;
    public bool isAttackCheck = false;
    int hp = 10;
    bool isStop = false;

    public GameObject PrefabBullet;
    public Transform BulletPoint;
    public float BulletDelay = 1.0f;
    public float BulletTime = 0f;
    bool isBullet = false;
    // Start is called before the first frame update
    void Start()
    {
        characterController = this.GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStop)
        {
            Walk();
            //StartCoroutine("Attack");
            Rotation();
            Attack();
        }
    }

    void Walk()
    {
        isWalk = false;

        if(Input.GetKey(KeyCode.W))
        {
            characterController.Move(this.transform.forward * Time.deltaTime * speed);
            isWalk = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            characterController.Move(-this.transform.forward * Time.deltaTime * speed);
            isWalk = true;
        }

        if ( Input.GetKey(KeyCode.A))
        {
            characterController.Move(-this.transform.right * Time.deltaTime * speed);
            isWalk = true;
        }
        else if( Input.GetKey(KeyCode.D))
        {
            characterController.Move(this.transform.right *Time.deltaTime * speed);
            isWalk = true;
        }

        if(isWalk)
        {
            animator.SetBool("isWalk",true);
        }
        else
        {
            animator.SetBool("isWalk",false);
        }
    }

    //IEnumerator Attack()
    //{
    //    if (Input.GetKeyDown(KeyCode.Mouse0))
    //    {
    //        animator.SetTrigger("isAttack");
    //        isAttackCheck = true;
    //        yield return new WaitForSeconds(0.5f);
    //        isAttackCheck = false;
    //    }
    //}

    void Rotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Plane plane = new Plane(Vector3.up, Vector3.zero);

        float rayLength;
        if(plane.Raycast(ray, out rayLength))
        {
            Vector3 mousePoint = ray.GetPoint(rayLength);

            this.transform.LookAt(new Vector3(mousePoint.x, this.transform.position.y, mousePoint.z));
        }
    }

    public void SetHp(int damage)
    {
        hp -= damage;
        if (hp <= 0 && !isStop)
        {
            hp = 0;
            Debug.Log("GameOver");
            animator.SetTrigger("Death");
            isStop = true;
        }
    }

    void Attack()
    {
        if(isBullet)
        {
            BulletTime += Time.deltaTime;

            if(BulletTime >= BulletDelay)
            {
                isBullet = false;
                BulletTime = 0;
            }
        }
        if(!isBullet)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                isBullet = true;
                animator.SetTrigger("isAttack");
                Invoke("SpawnBullet", 0.2f);
            }
        }
    }

    void SpawnBullet()
    {
        Instantiate(PrefabBullet, BulletPoint.position, this.transform.rotation);
    }

    
}
