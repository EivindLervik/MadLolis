using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

    public CanvasScript canvas;

    private bool inside;
    private Interactable interactable;

    private CharacterInput characterInput;

    private void Start()
    {
        characterInput = GetComponent<CharacterInput>();
        canvas.SetCharacterInput(characterInput);

        Cursor.visible = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        inside = true;

        string hitTag = other.transform.tag;

        switch (hitTag)
        {
            case "Interactable":
                interactable = other.GetComponent<Interactable>();
                canvas.PromptInteraction("Use " + interactable.objectType.ToString(), interactable.activationKey);
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        inside = false;
        canvas.HideInteraction();
        interactable = null;
    }

    private void Update()
    {
        if (inside)
        {
            if (Input.GetKeyDown(interactable.activationKey))
            {
                switch (interactable.objectType)
                {
					case InteractableObject.Elevator:
						
						break;
                    case InteractableObject.Lathe:
                        canvas.OpenLatheMenu();
                        break;
					case InteractableObject.Storage:
						canvas.OpenStorageMenu ((Storage)interactable);
						break;
					case InteractableObject.Welder:
						
						break;
                }
            }
        }
    }

}
