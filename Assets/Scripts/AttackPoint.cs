using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackPoint : MonoBehaviour
{
    public Enemy enemy;
    public Player player;
    public int damage;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

    }
    public enum Char
    {
        Player,
        Enemy,
    }
    public Char Character;
    private void OnTriggerEnter(Collider other)
    {
        switch (Character)
        {
            case Char.Player:
                if (other.CompareTag("Enemy"))
                {
                    if (player.isAttackCheck)
                    {
                        Debug.Log(other.tag);
                        player.isAttackCheck = false;
                        enemy.SetHp(damage);
                    }
                }
                break;
            case Char.Enemy:
                if (other.CompareTag("Player"))
                {
                    if (enemy.isAttackCheck)
                    {
                        Debug.Log(other.tag);
                        enemy.isAttackCheck = false;
                        player.SetHp(damage);
                    }
                }
                break;
        }
    }
}
