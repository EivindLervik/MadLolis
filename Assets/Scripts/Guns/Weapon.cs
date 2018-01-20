using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public Barrel barrel;
	public Magazine magazine;
	public Muzzle muzzle;
	public Scope scope;
	public Stock stock;
    public Reloader reloader;

    public CanvasScript canvas;

    private bool canFire;

    private void Start()
    {
        canFire = true;
        Chamber();
    }

    public bool Fire()
    {
        if (canFire)
        {
            if (barrel.Fire())
            {
                canFire = false;
                canvas.UpdateCrosshair(new List<string>() { (barrel.recoil * stock.recoilModifier).ToString(), 3.ToString() });
                StartCoroutine("Fire_C");
                return true;
            }
            
        }
        return false;
    }
    private IEnumerator Fire_C()
    {
        yield return new WaitForSeconds(barrel.recoil * stock.recoilModifier);
        reloader.Reload();
        yield return new WaitForSeconds(reloader.GetReloadTime());
        canFire = true;
        Chamber();
    }

    private void Chamber()
    {
        int a = magazine.Use(barrel.chamberNumber);
        barrel.Chamber(a);
    }

    public float GetDamage()
    {
        return barrel.GetDamage();
    }

    public void Reload()
    {

    }

    public float GetRange()
    {
        // Change to actual range
        return 1000;
    }

}
