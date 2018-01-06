using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableObject
{
    Lathe, Elevator, Welder, Storage
}

public enum InteractableType
{
    Use, Open, Activate
}

public class Interactable : MonoBehaviour {

    public InteractableObject objectType;
    public InteractableType interactableType;
	public string objectName;
    public KeyCode activationKey;

}
