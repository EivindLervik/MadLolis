using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_Car : GUI_Element {

    [Header("Car")]
    public RectTransform speedometerNeedle;

    public override void UpdateGUI(List<string> data)
    {
        float speed = float.Parse(data[0]);
        float index = speed / 180.0f;
        speedometerNeedle.localEulerAngles = new Vector3(0.0f, 0.0f, (index * -180.0f) + 90.0f);
    }
}
