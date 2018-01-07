using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour {

    public CanvasScript canvas;

    public string steerAxis;
    public string throttleAxis;

    private BoatMotor boatMotor;

    // Use this for initialization
    void Start()
    {
        boatMotor = GetComponent<BoatMotor>();
        //canvas.ShowSpeedometer();
    }

    // Update is called once per frame
    void Update()
    {
        boatMotor.UpdateSteer(Input.GetAxis(steerAxis));
        boatMotor.UpdateThrottle(Input.GetAxis(throttleAxis));

        //canvas.UpdateSpeedometer(boatMotor.GetSpeed());
    }

}
