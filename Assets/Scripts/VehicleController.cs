using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour {

    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public List<Gear> gears;

    private Rigidbody body;
    private AudioSource audioSource;
    private int currentGear;

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
        float speed = body.velocity.magnitude;

        if(speed > gears[currentGear - 1].highSpeed)
        {
            if(currentGear < gears.Count)
            {
                currentGear++;
            }
        }
        else if (speed < gears[currentGear - 1].lowSpeed)
        {
            if (currentGear > 1)
            {
                currentGear--;
            }
        }

        if(currentGear == 1)
        {
            audioSource.pitch = (((speed - gears[currentGear - 1].lowSpeed) / (gears[currentGear - 1].highSpeed - gears[currentGear - 1].lowSpeed)) * 2.5f) + 0.5f;
        }
        else
        {
            audioSource.pitch = (((speed - gears[currentGear - 1].lowSpeed) / (gears[currentGear - 1].highSpeed - gears[currentGear - 1].lowSpeed)) * 1.5f) + 1.5f;

        }

        print(speed);
    }

    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    public void FixedUpdate()
    {
        float speed = body.velocity.magnitude;

        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor * gears[currentGear - 1].effect.Evaluate((speed - gears[currentGear - 1].lowSpeed) / (gears[currentGear - 1].highSpeed - gears[currentGear - 1].lowSpeed));
                axleInfo.rightWheel.motorTorque = motor * gears[currentGear - 1].effect.Evaluate((speed - gears[currentGear - 1].lowSpeed) / (gears[currentGear - 1].highSpeed - gears[currentGear - 1].lowSpeed));
            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
    }
}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
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