using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableObject
{
    Lathe, Elevator, Welder
}

public class Interactable : MonoBehaviour {

    public InteractableObject objectType;
    public KeyCode activationKey;

}
