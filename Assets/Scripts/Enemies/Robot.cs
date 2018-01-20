using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : Enemy {

    [Header("Robot")]
    public Transform model;

    public GameObject destroyEffect;
    public EnemyLoot loot;

    protected Transform target;
    protected bool persuit;
    private List<Enemy> adjacentEnemies;

    protected override void Setup()
    {
        base.Setup();
        adjacentEnemies = new List<Enemy>();
    }

    protected override void Die()
    {
        base.Die();

        WarnAdjacentEnemies();

        Instantiate(destroyEffect, model.position, Quaternion.identity);
        Instantiate(loot.lootObject, model.position, Quaternion.identity).GetComponentInChildren<Storage>().items = loot.items;
        Destroy(gameObject);
    }

    private void WarnAdjacentEnemies()
    {
        if (target)
        {
            foreach (Enemy e in adjacentEnemies)
            {
                e.DetectPlayer(target);
            }
        }
    }

    public override void DetectPlayer(Transform player)
    {
        target = player;
        persuit = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            target = other.transform;
        }
        else if (other.tag.Equals("Enemy"))
        {
            
            adjacentEnemies.Add(other.gameObject.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            target = null;
            persuit = false;
        }
        else if (other.tag.Equals("Enemy"))
        {
            adjacentEnemies.Remove(other.gameObject.GetComponent<Enemy>());
        }
    }

}
