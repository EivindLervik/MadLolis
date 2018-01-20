using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_Element : MonoBehaviour {

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public virtual void UpdateGUI(List<string> data)
    {

    }

}
