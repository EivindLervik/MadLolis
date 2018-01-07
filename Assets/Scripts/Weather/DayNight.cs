using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour {

    public float daySpeed;
    public Light sun;
    public Transform sunTransform;


    private void Update()
    {
        sunTransform.Rotate(Vector3.forward, daySpeed * Time.deltaTime);

        float eff;
        if(sunTransform.localEulerAngles.z > 180.0f)
        {
            eff = Mathf.Abs(((Mathf.Clamp(sunTransform.localEulerAngles.z, 250.0f, 270.0f) - 250.0f) / 20.0f) - 1);
        }
        else
        {
            eff = (Mathf.Clamp(sunTransform.localEulerAngles.z, 70.0f, 90.0f) - 70.0f) / 20.0f;
        }

        sun.intensity = eff;
    }

}
