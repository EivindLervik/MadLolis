using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : GunPart {

    [Header("Barrel Details")]
    public string ammoType;
	public float accuracy;
	public float speedModefier;
    public float recoil;
    public int chamberNumber;

    [Header("Sound Effects")]
    public AudioClip fire;
    public AudioClip click;

    [Header("Connectors")]
    public Transform magazineConnector;
	public Transform muzzleConnector;

    private int chambered;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public bool Fire()
    {
        bool ok = false;
        if (chambered > 0)
        {
            audioSource.clip = fire;
            chambered--;
            ok = true;
        }
        else
        {
            audioSource.clip = click;
        }

        audioSource.Play();

        return ok;
    }

    public void Chamber(int amount)
    {
        chambered = Mathf.Min(amount, chamberNumber);
    }

    public int GetChambered()
    {
        return chambered;
    }
}
