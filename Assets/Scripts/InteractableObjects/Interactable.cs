using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableObject
{
    Lathe, Elevator, Welder, Storage, Anvil, Arcade
}

public enum InteractableType
{
    Use, Open, Activate, Search, Play
}

public class Interactable : MonoBehaviour {

    public InteractableObject objectType;
    public InteractableType interactableType;
	public string objectName;
    public KeyCode activationKey;

}
