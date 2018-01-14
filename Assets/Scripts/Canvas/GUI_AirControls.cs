using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_AirControls : GUI_Element {

    [Header("Altimeter")]
    public Text altimeterText;
    public RectTransform altimeterBar;
    public Gradient altimeterColor;

    [Header("Horizontal Speed")]
    public Text hSpeedText;
    public RectTransform hSpeedNeedle;

    [Header("Vertical Speed")]
    public Text vSpeedText;
    public RectTransform vSpeedBar;
    public Gradient vSpeedColor;

    public override void UpdateGUI(List<string> data)
    {
        float altimeter = float.Parse(data[0]);
        float altimeterMax = float.Parse(data[1]);
        float hSpeed = float.Parse(data[2]);
        float compasAngle = float.Parse(data[3]);
        float vSpeed = float.Parse(data[4]);
        float vSpeedMax = float.Parse(data[5]);

        // Altimeter
        altimeterText.text = Mathf.RoundToInt(altimeter).ToString() + "m";
        altimeterBar.transform.localScale = new Vector3(1.0f, Mathf.Clamp(altimeter / altimeterMax, 0.0f, 1.0f), 1.0f);
        altimeterBar.GetComponent<Image>().color = altimeterColor.Evaluate(altimeter / altimeterMax);

        // Horizontal Speed
        hSpeedText.text = Mathf.RoundToInt(hSpeed * 3.6f).ToString() + "km/t";
        hSpeedNeedle.localEulerAngles = new Vector3(hSpeedNeedle.localEulerAngles.x, hSpeedNeedle.localEulerAngles.y, compasAngle);

        // Vertical Speed
        vSpeedText.text = Mathf.RoundToInt(vSpeed).ToString() + "m/s";
        vSpeedBar.transform.localScale = new Vector3(1.0f, Mathf.Clamp(vSpeed / vSpeedMax, -1.0f, 1.0f), 1.0f);
        vSpeedBar.GetComponent<Image>().color = vSpeedColor.Evaluate(Mathf.Abs(vSpeed) / vSpeedMax);
    }

}
