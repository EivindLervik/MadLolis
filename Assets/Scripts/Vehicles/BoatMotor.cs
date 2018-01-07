using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMotor : MonoBehaviour {

    public Transform model;

    [Header("Scooter")]
    public float maxPitch;
    public float boyancy;
    public AudioClip idle_SFX;
    public AudioClip throttle_SFX;

    [Header("Throttle")]
    public float acceleration;
    public AnimationCurve accelerationEffect;
    public AnimationCurve backingEffect;
    public AnimationCurve planarAngle;
    public AnimationCurve bobbingSpeed;

    [Header("Steering")]
    public float turnAcceleration;
    public float turmAccelerator_Air;
    public AnimationCurve turnEffect;
    public float turnAngle;
    public float driftCanceler;
    private float turn;
    private float airTurn;

    private Rigidbody body;
    private AudioSource audioSource;
    private Animator bobAnimator;

    private float throttle;
    private float steer;
    private float enginePitch;

    private bool onWater;

    void Start () {
        body = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        bobAnimator = model.GetComponent<Animator>();
    }

	void FixedUpdate () {
        

        if (onWater)
        {
            
            Vector3 force = Vector3.forward * acceleration * throttle;

            if (throttle >= 0.0f)
            {
                enginePitch = Mathf.Lerp(enginePitch, Mathf.Abs(throttle) * (maxPitch - 1.0f), Time.deltaTime);
                audioSource.pitch = 1.0f + enginePitch;
                force *= accelerationEffect.Evaluate(GetSpeed());
            }
            else if (throttle < 0.0f)
            {
                enginePitch = Mathf.Lerp(enginePitch, Mathf.Abs(throttle) * ((maxPitch/1.5f) - 1.0f), Time.deltaTime);
                audioSource.pitch = 1.0f + enginePitch;
                if (Vector3.Dot(body.velocity.normalized, transform.forward) > 1.0f)
                {
                    force *= backingEffect.Evaluate(GetSpeed());
                }
                else
                {
                    force *= 0.6f;
                }
            }

            

            if (transform.position.y < 0.0f)
            {
                force.y = Mathf.Abs(transform.position.y) * boyancy;
                body.velocity = new Vector3(body.velocity.x, body.velocity.y * 0.85f, body.velocity.z);
            }

            float f = Vector3.Dot(body.velocity.normalized, transform.right);
            Vector3 localForce = transform.InverseTransformDirection(body.velocity);
            localForce.x *= (1 - (Mathf.Abs(f) * 0.01f * driftCanceler));
            body.velocity = transform.TransformDirection(localForce);

            body.AddRelativeForce(force);

            Vector3 turnForce = Vector3.up * turnAcceleration * steer * turnEffect.Evaluate(GetSpeed()) * Mathf.Ceil(Vector3.Dot(transform.forward, body.velocity));
            body.AddRelativeTorque(turnForce);

            // Animate Turn
            turn = Mathf.Lerp(turn, steer, Time.fixedDeltaTime);
            model.localEulerAngles = new Vector3(-planarAngle.Evaluate(GetSpeed()), 0.0f, turnAngle * -turn * turnEffect.Evaluate(GetSpeed()));

            // Animate Bob
            bobAnimator.speed = bobbingSpeed.Evaluate(GetSpeed());
        }
        else
        {
            enginePitch = Mathf.Lerp(enginePitch, Mathf.Abs(throttle) * ((maxPitch * 1.5f) - 1.0f), Time.deltaTime);
            audioSource.pitch = 1.0f + enginePitch;

            Vector3 turnForce = Vector3.up * turmAccelerator_Air * steer;
            body.AddRelativeTorque(turnForce);

            bobAnimator.speed = 0.0f;
        }
        
    }

    public float GetSpeed()
    {
        Vector3 b = body.velocity;
        b.y = 0.0f;
        return b.magnitude * 3.6f;
    }

    public void UpdateThrottle(float throttle)
    {
        this.throttle = throttle;
    }

    public void UpdateSteer(float steer)
    {
        this.steer = steer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag.Equals("Water")){
            onWater = true;
            airTurn = 0.0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag.Equals("Water"))
        {
            onWater = false;
            turn = 0.0f;
        }
    }
}
