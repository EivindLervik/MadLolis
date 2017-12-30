using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour {

	public float lookingSensitivity;
	public Transform cameraRig;

	private CharacterMover motor;

	// Use this for initialization
	void Start () {
		motor = GetComponent<CharacterMover> ();
	}
	
	// Update is called once per frame
	void Update () {
		Turn ();
		motor.UpdateMovement (new Vector3(Input.GetAxis ("Horizontal"), 0.0f, Input.GetAxis ("Vertical")));
		if(Input.GetButtonDown ("Jump")){
			motor.Jump ();
		}
	}

	private void Turn(){
		transform.Rotate (transform.up, Input.GetAxis ("Mouse X") * lookingSensitivity);
		cameraRig.Rotate (Vector3.right, Input.GetAxis ("Mouse Y") * lookingSensitivity);
	}
}
