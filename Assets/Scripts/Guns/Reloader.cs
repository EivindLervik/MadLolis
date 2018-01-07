using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reloader : GunPart {

    private Animation reloadAnimation;

    private void Start()
    {
        reloadAnimation = GetComponent<Animation>();
    }

    public void Reload()
    {
        reloadAnimation.Play();
    }

    public virtual float GetReloadTime()
    {
        return 0.0f;
    }

}
