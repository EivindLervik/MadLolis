using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : Enemy {

	// Use this for initialization
	void Awake () {
        inventory = GetComponentInChildren<Storage>();
        inventory.gameObject.SetActive(false);
        Setup();
    }

    protected override void Die()
    {
        base.Die();

        inventory.gameObject.SetActive(true);
    }

}
