using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    private EnemyBodyPart[] bodyParts;
    private Animator animator;
    private Storage inventory;

    protected void Setup()
    {
        bodyParts = GetComponentsInChildren<EnemyBodyPart>();
        animator = GetComponent<Animator>();
        inventory = GetComponentInChildren<Storage>();

        inventory.gameObject.SetActive(false);

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
            animator.SetTrigger("Die");
            inventory.gameObject.SetActive(true);

            foreach(EnemyBodyPart ebp in bodyParts)
            {
                ebp.GetComponent<Collider>().enabled = false;
            }
        }
    }

}
