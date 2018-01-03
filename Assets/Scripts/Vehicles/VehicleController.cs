using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour {

    public CanvasScript canvas;

	public string steerAxis;
	public string throttleAxis;

    private VehicleMotor vehicleMotor;

	// Use this for initialization
	void Start () {
        vehicleMotor = GetComponent<VehicleMotor>();
        canvas.ShowSpeedometer();
	}
	
	// Update is called once per frame
	void Update () {
		vehicleMotor.UpdateSteer (Input.GetAxis(steerAxis));
		vehicleMotor.UpdateThrottle (Input.GetAxis(throttleAxis));

        canvas.UpdateSpeedometer(vehicleMotor.GetSpeed());
	}
}
