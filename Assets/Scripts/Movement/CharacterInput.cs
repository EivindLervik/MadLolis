using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour {

	public float lookingSensitivity;
	public Transform cameraRig;

	private CharacterMover motor;
    private Weapon weapon;
    private Camera camera;

    private bool allowInput;

    // Use this for initialization
    void Start () {
		motor = GetComponent<CharacterMover> ();
        weapon = GetComponentInChildren<Weapon> ();
        camera = cameraRig.GetComponentInChildren<Camera>();

        allowInput = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (allowInput)
        {
            Turn();

            motor.UpdateMovement(new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")));
            motor.Turn(Input.GetAxis("Mouse X") * lookingSensitivity);

            if (Input.GetButtonDown("Jump"))
            {
                motor.Jump();
            }

            // Guns
            if (Input.GetButton("Fire1"))
            {
                if (weapon.Fire())
                {
                    Ray ray = camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, weapon.GetRange()))
                    {
                        switch (hit.transform.tag)
                        {
                            case "PhysObj":
                                Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                                rb.AddExplosionForce(10000, hit.point, 10.0f);
                                break;
                            case "DesObj":
                                Destroy(hit.transform.gameObject);
                                break;
                        }

                    }
                    else
                    {
                        print("I'm looking at nothing!");
                    }
                }
            }
            if (Input.GetButtonDown("Reload"))
            {
                weapon.Reload();
            }
        }
    }

	private void Turn(){
		cameraRig.Rotate (Vector3.right, Input.GetAxis ("Mouse Y") * lookingSensitivity);
	}

    public void AllowInput(bool allow)
    {
        allowInput = allow;
    }
}
