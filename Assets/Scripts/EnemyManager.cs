using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    List<Transform> enemyResponse = new List<Transform>();
    public GameObject PrefabEnemy;
    List<Enemy> enmies = new List<Enemy>();
    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Respawn");

        foreach (GameObject obj in objs)
        {
            enemyResponse.Add(obj.transform);
        }
        CreateEnemy();
    }

    public void CreateEnemy()
    {
        foreach (Transform tr in enemyResponse)
        {
            Instantiate(PrefabEnemy, tr.position, PrefabEnemy.transform.rotation);
        }
    }

}
