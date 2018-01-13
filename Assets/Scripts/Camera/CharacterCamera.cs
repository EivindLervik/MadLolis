using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour {

    public Transform player;
    public LayerMask checkCollisionOn;
    public float wallDistance;
    public Vector3 targetOffset;

	void Start () {
		
	}

	void Update () {
        Ray ray = new Ray(player.position, (transform.position - player.position).normalized);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit,(player.TransformPoint(targetOffset) - player.position).magnitude, checkCollisionOn))
        {
            transform.position = hit.point + ((player.position - transform.position).normalized * wallDistance);
        }
        else
        {
            transform.localPosition = targetOffset;
        }
	}
}
