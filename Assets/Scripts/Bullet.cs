using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10.0f;
    public float endTime = 2.0f;        //총할이 사라지는 시간
    public float lifeTime = 0f;         //총알이 나타나고 경과된 시간
    public int damage = 1;

    // Update is called once per frame
    void Update()
    {
        lifeTime += Time.deltaTime;
        transform.Translate(Vector3.forward * Time.deltaTime * speed);//정면으로 이동

        if(lifeTime >= endTime)
        {
            lifeTime = 0f;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy");

            Enemy enemy = other.GetComponent<Enemy>();
            enemy.SetHp(damage);
            Destroy(this.gameObject);
        }
    }
}
