using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_Interaction : GUI_Element {

    [Header("Interaction")]
    public Text interaction_Text;
    public Text interaction_Key;

    public void OpenInteraction(string text, KeyCode key)
    {
        interaction_Text.text = text;
        interaction_Key.text = key.ToString();
    }
}
