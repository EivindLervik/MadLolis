using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterController : MonoBehaviour {

    public string frontback;
    public string sideways;
    public string updown;
    public string rotation;

    private HelicopterMotor helicopterMotor;

	void Start () {
        helicopterMotor = GetComponent<HelicopterMotor>();
	}

	void Update () {
        helicopterMotor.UpdateFrontBack(Input.GetAxis(frontback));
        helicopterMotor.UpdateLeftRight(Input.GetAxis(sideways));
        helicopterMotor.UpdateUpDown(Input.GetAxis(updown));
        helicopterMotor.UpdateRotation(Input.GetAxis(rotation));
	}
}
