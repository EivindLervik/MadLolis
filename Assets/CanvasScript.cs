using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour {

    [Header("Crosshair")]
    public RectTransform up;
    public RectTransform down;
    public RectTransform left;
    public RectTransform right;

    [Header("Interaction")]
    public GameObject interaction_MENU;
    public Text interaction_Text;
    public Text interaction_Key;

    [Header("Lathe")]
    public GameObject lathe_MENU;

    private Vector3 up_SP;
    private Vector3 down_SP;
    private Vector3 left_SP;
    private Vector3 right_SP;

    private float fadeback;
    private CharacterInput characterInput;

    private void Start()
    {
        up_SP = up.position;
        down_SP = down.position;
        left_SP = left.position;
        right_SP = right.position;

        CLoseAllMenues();
    }

    private void Update()
    {
        if(up.position != up_SP)
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

    public void SetCharacterInput(CharacterInput characterInput)
    {
        this.characterInput = characterInput;
    }

    public void OpenLatheMenu()
    {
        characterInput.AllowInput(false);
        HideCrosshair();
        HideInteraction();
        lathe_MENU.SetActive(true);
    }

    public void PromptInteraction(string text, KeyCode key)
    {
        interaction_MENU.SetActive(true);
        interaction_Text.text = text;
        interaction_Key.text = key.ToString();
    }

    public void HideInteraction()
    {
        interaction_MENU.SetActive(false);
    }

    public void CrosshairEffect(float magnitude, float fadeback)
    {
        this.fadeback = fadeback;

        float magnitudeModifier = 25.0f;
        Vector3 effect = Vector3.up * magnitude * magnitudeModifier;

        up.Translate(effect);
        down.Translate(effect);
        left.Translate(effect);
        right.Translate(effect);
    }

    private void CLoseAllMenues()
    {
        interaction_MENU.SetActive(false);
        lathe_MENU.SetActive(false);

        if(characterInput != null)
        {
            characterInput.AllowInput(true);
        }
    }

    private void HideCrosshair()
    {
        up.gameObject.SetActive(false);
        down.gameObject.SetActive(false);
        left.gameObject.SetActive(false);
        right.gameObject.SetActive(false);
    }

    private void ShowCrosshair()
    {
        up.gameObject.SetActive(true);
        down.gameObject.SetActive(true);
        left.gameObject.SetActive(true);
        right.gameObject.SetActive(true);
    }


}
