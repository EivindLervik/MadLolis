using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyPart : MonoBehaviour {

    [Range(0.0f, 2.0f)]
    public float damageModefier;

    private Enemy callBack;

    public void SetCallBack(Enemy callBack)
    {
        this.callBack = callBack;
    }

    public void TakeDamage(float damage)
    {
        callBack.TakeDamage(damage * damageModefier);
    }

}
