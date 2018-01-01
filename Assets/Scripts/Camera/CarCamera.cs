using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCamera : MonoBehaviour {

    public Transform target;
    public Vector3 relativePosition;
    public float followEffect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position, target.position + target.TransformDirection(relativePosition), Time.deltaTime * followEffect);
        transform.LookAt(target.position + transform.up);
	}
}
