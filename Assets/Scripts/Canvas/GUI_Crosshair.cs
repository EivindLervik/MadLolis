using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_Crosshair : GUI_Element {

    [Header("Crosshair")]
    public RectTransform up;
    public RectTransform down;
    public RectTransform left;
    public RectTransform right;

    private Vector3 up_SP;
    private Vector3 down_SP;
    private Vector3 left_SP;
    private Vector3 right_SP;

    private float fadeback;

    void Start () {
        if (up != null && down != null && left != null && right != null)
        {
            up_SP = up.position;
            down_SP = down.position;
            left_SP = left.position;
            right_SP = right.position;
        }
    }
    public override void UpdateGUI(List<string> data){
        float magnitude = float.Parse(data[0]);
        float fadeback = float.Parse(data[1]);

        this.fadeback = fadeback;

        float magnitudeModifier = 25.0f;
        Vector3 effect = Vector3.up * magnitude * magnitudeModifier;

        up.Translate(effect);
        down.Translate(effect);
        left.Translate(effect);
        right.Translate(effect);
    }

    void Update () {
        if (up != null && down != null && left != null && right != null)
        {
            if (up.position != up_SP)
            {
                up.position = Vector3.Lerp(up.position, up_SP, Time.deltaTime * fadeback);
            }
            if (down.position != down_SP)
            {
                down.position = Vector3.Lerp(down.position, down_SP, Time.deltaTime * fadeback);
            }
            if (left.position != left_SP)
            {
                left.position = Vector3.Lerp(left.position, left_SP, Time.deltaTime * fadeback);
            }
            if (right.position != right_SP)
            {
                right.position = Vector3.Lerp(right.position, right_SP, Time.deltaTime * fadeback);
            }
        }
    }
}
