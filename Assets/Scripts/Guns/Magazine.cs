using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : GunPart {

	public string ammoType;
	public int capacity;
	public int current;

    public int Use(int amount)
    {
        int a = Mathf.Min(amount, current);
        current -= a;

        return a;
    }

    public int Reload(int amount)
    {
        int a = Mathf.Min(amount, capacity);
        int diff = amount - capacity;
        current = a;

        if(diff >= 0)
        {
            return diff;
        }
        else
        {
            return 0;
        }
    }

}
