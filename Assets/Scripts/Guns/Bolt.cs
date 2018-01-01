using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : Reloader {

	public float backTime;
	public float waitTime;
	public float frontTime;

    public override float GetReloadTime()
    {
        return backTime + waitTime + frontTime;
    }
}
