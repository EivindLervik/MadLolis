using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    protected EnemyBodyPart[] bodyParts;
    protected Animator animator;
    protected Storage inventory;
    protected Rigidbody body;

    protected virtual void Setup()
    {
        bodyParts = GetComponentsInChildren<EnemyBodyPart>();
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();

        foreach (EnemyBodyPart ebp in bodyParts)
        {
            ebp.SetCallBack(this);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0.0f)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        animator.SetTrigger("Die");

        foreach (EnemyBodyPart ebp in bodyParts)
        {
            ebp.GetComponent<Collider>().enabled = false;
        }
    }

    public virtual void DetectPlayer(Transform player)
    {

    }

}

[System.Serializable]
public class EnemyLoot
{
    public GameObject lootObject;
    public List<StorageEntry> items;
}