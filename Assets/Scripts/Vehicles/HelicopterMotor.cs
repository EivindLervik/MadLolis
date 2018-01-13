using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterMotor : MonoBehaviour {

    public Transform model;
    public Transform rotor;

    public bool on;

    [Header("Front and Back")]
    public float fbAcceleration;
    public float fbResponse;
    public float fbLeaning;
    public float fbLeaningResponse;

    [Header("Left and Right")]
    public float lrAcceleration;
    public float lrResponse;
    public float lrLeaning;
    public float lrLeaningResponse;

    [Header("Rotation")]
    public float rotationAcceleration;

    [Header("Up and Down")]
    public float rpmChange;
    public float rpmChangeResponse;
    public float stableRPM;
    public AnimationCurve upThrottleModifier;


    private Rigidbody body;
    private AudioSource audioSource;

    private float frontback;
    private float leftright;
    private float updown;
    private float rotation;

    private float rpm;
    private float throttle;
    private float sideSpeed;

    private Vector3 modelRotation;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        rotor.Rotate(transform.up, rpm);
    }

    void FixedUpdate () {
        float change = updown * rpmChange;

        if(updown > 0.0f) {
            change *= upThrottleModifier.Evaluate(transform.position.y);
        }

        // Set speeds
        if (on)
        {
            rpm = Mathf.Lerp(rpm, stableRPM + change, Time.deltaTime * rpmChangeResponse);
            throttle = Mathf.Lerp(throttle, frontback, Time.deltaTime * fbResponse);
            sideSpeed = Mathf.Lerp(sideSpeed, leftright, Time.deltaTime * lrResponse);
        }
        else
        {
            rpm = Mathf.Lerp(rpm, 0.0f, Time.deltaTime * rpmChangeResponse);
            throttle = Mathf.Lerp(throttle, 0.0f, Time.deltaTime * fbResponse);
            sideSpeed = Mathf.Lerp(sideSpeed, 0.0f, Time.deltaTime * lrResponse);
        }

        // Add forces
        Vector3 force = new Vector3(sideSpeed * lrAcceleration, (rpm - stableRPM), throttle * fbAcceleration);
        body.AddRelativeForce(force);

        // Rotation
        Vector3 rot = (Vector3.up * rotation * rotationAcceleration) / Mathf.PI;
        body.AddTorque(rot);

        // Soundeffects
        audioSource.pitch = (rpm / stableRPM);
        audioSource.volume = Mathf.Clamp((audioSource.pitch * 2.0f), 0.0f, 1.0f);


        // Animate model
        modelRotation.x = Mathf.Lerp(modelRotation.x, fbLeaning * throttle, Time.deltaTime * fbLeaningResponse);
        modelRotation.z = Mathf.Lerp(modelRotation.z, lrLeaning * -sideSpeed, Time.deltaTime * lrLeaningResponse);
        model.localEulerAngles = modelRotation;
    }



    public void UpdateFrontBack(float frontback)
    {
        this.frontback = frontback;
    }

    public void UpdateLeftRight(float leftright)
    {
        this.leftright = leftright;
    }

    public void UpdateUpDown(float updown)
    {
        this.updown = updown;
    }

    public void UpdateRotation(float rotation)
    {
        this.rotation = rotation;
    }
}
