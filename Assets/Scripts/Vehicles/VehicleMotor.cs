using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMotor : MonoBehaviour {

    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public float steeringForce;
    public List<Gear> gears;

    private Rigidbody body;
    private AudioSource audioSource;
    private int currentGear;

	private float throttle;
	private float steer;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        foreach(AxleInfo info in axleInfos)
        {
            info.leftWheel.ConfigureVehicleSubsteps(5, 12, 15);
            info.rightWheel.ConfigureVehicleSubsteps(5, 12, 15);
        }

        currentGear = 1;
    }

    private void Update()
    {
        float speed = body.velocity.magnitude * Mathf.Round(transform.InverseTransformDirection(body.velocity.normalized).z);

        if (speed > gears[currentGear].highSpeed)
        {
            if (currentGear < gears.Count)
            {
                currentGear++;
            }
        }
        else if (speed < gears[currentGear].lowSpeed)
        {
            if (currentGear > 0)
            {
                currentGear--;
            }
        }

        if (currentGear == 0)
        {
            audioSource.pitch = ((speed / (gears[currentGear].highSpeed + gears[currentGear].lowSpeed)) * 2.5f) + 0.5f;
        }
        else if (currentGear == 1)
        {
            audioSource.pitch = (((speed - gears[currentGear].lowSpeed) / (gears[currentGear].highSpeed - gears[currentGear].lowSpeed)) * 2.5f) + 0.5f;

        }
        else if (currentGear > 1)
        {
            audioSource.pitch = (((speed - gears[currentGear].lowSpeed) / (gears[currentGear].highSpeed - gears[currentGear].lowSpeed)) * 1.5f) + 1.5f;

        }
    }

    public float GetSpeed()
    {
        Vector3 b = body.velocity;
        b.y = 0.0f;
        return b.magnitude * 3.6f;
    }

    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider, Transform visualWheel)
    {
        if (visualWheel == null)
        {
            return;
        }

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    private float steeringAngle;
    public void FixedUpdate()
    {
        float speed = body.velocity.magnitude;

		float motor = maxMotorTorque * throttle;
		steeringAngle = Mathf.Lerp(steeringAngle, steer, Time.deltaTime * steeringForce);
        float steering = maxSteeringAngle * steeringAngle;

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                if(currentGear == 0)
                {
                    axleInfo.leftWheel.motorTorque = motor * gears[currentGear].effect.Evaluate(speed / (gears[currentGear].highSpeed - gears[currentGear].lowSpeed));
                    axleInfo.rightWheel.motorTorque = motor * gears[currentGear].effect.Evaluate(speed / (gears[currentGear].highSpeed - gears[currentGear].lowSpeed));
                }
                else
                {
                    axleInfo.leftWheel.motorTorque = motor * gears[currentGear].effect.Evaluate((speed - gears[currentGear].lowSpeed) / (gears[currentGear].highSpeed - gears[currentGear].lowSpeed));
                    axleInfo.rightWheel.motorTorque = motor * gears[currentGear].effect.Evaluate((speed - gears[currentGear].lowSpeed) / (gears[currentGear].highSpeed - gears[currentGear].lowSpeed));
                }
            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel, axleInfo.leftWheel_Visual);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel, axleInfo.rightWheel_Visual);
        }
    }

	public void UpdateThrottle(float throttle){
		this.throttle = throttle;
	}

	public void UpdateSteer(float steer){
		this.steer = steer;
	}
}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public Transform leftWheel_Visual;
    public Transform rightWheel_Visual;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}

[System.Serializable]
public class Gear
{
    public AnimationCurve effect;
    public float lowSpeed;
    public float highSpeed;
}