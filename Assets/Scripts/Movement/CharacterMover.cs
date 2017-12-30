using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour {

	private Rigidbody body;
	private CapsuleCollider collider;
	private Vector3 desiredMovement;
	private bool grounded;

	[Header("Walking")]
	public float maxMoveSpeed;
	public float acceleration;
	public float deacceleration;

	[Header("Jumping")]
	public float jumpForce;
	public LayerMask groundMask;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody> ();
		collider = GetComponent<CapsuleCollider> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 force = new Vector3 ();

		force = desiredMovement * acceleration;

		body.AddRelativeForce(force);

		if (body.velocity.x > maxMoveSpeed) {
			Vector3 fixedVelocity = body.velocity;
			fixedVelocity.x = maxMoveSpeed;
			body.velocity = fixedVelocity;
		}
		if (body.velocity.z > maxMoveSpeed) {
			Vector3 fixedVelocity = body.velocity;
			fixedVelocity.z = maxMoveSpeed;
			body.velocity = fixedVelocity;
		}

		// Check if landed
		Ray ray = new Ray(transform.position + (transform.up * collider.radius * 2.0f), -transform.up);	
		RaycastHit hit;
		if (Physics.SphereCast (ray, collider.radius, out hit, collider.radius + 0.1f, groundMask)) {
			grounded = true;
		} else {
			grounded = false;
		}
	}

	public void Jump(){
		if (grounded) {
			body.AddForce (transform.up * jumpForce);
		}
	}

	public void UpdateMovement(Vector3 move){
		desiredMovement.x = move.x;
		desiredMovement.y = 0.0f;
		desiredMovement.z = move.z;
	}
}
