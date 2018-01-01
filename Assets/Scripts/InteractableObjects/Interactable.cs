using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableObject
{
    Lathe, Elevator, Welder, Storage
}

public class Interactable : MonoBehaviour {

    public InteractableObject objectType;
	public string objectName;
    public KeyCode activationKey;

}
