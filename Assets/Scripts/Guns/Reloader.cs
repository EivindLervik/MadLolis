using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reloader : GunPart {

    private Animation animation;

    private void Start()
    {
        animation = GetComponent<Animation>();
    }

    public void Reload()
    {
        animation.Play();
    }

    public virtual float GetReloadTime()
    {
        return 0.0f;
    }

}
