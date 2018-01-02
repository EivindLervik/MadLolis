﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour {

    public CanvasScript canvas;

    private VehicleMotor vehicleMotor;

	// Use this for initialization
	void Start () {
        vehicleMotor = GetComponent<VehicleMotor>();
        canvas.ShowSpeedometer();
	}
	
	// Update is called once per frame
	void Update () {
        canvas.UpdateSpeedometer(vehicleMotor.GetSpeed());
	}
}
