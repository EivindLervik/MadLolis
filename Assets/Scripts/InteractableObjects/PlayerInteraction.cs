using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

    public CanvasScript canvas;

    private bool inside;
    private bool isInMenu;
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
                canvas.PromptInteraction(interactable.interactableType + " " + interactable.objectName, interactable.activationKey);
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
                        if (isInMenu)
                        {
                            canvas.CloseLatheMenu();
                        }
                        else
                        {
                            canvas.OpenLatheMenu();
                        }
                        isInMenu = !isInMenu;
                        break;
					case InteractableObject.Storage:
                        if (isInMenu)
                        {
                            canvas.CloseStorageMenu();
                        }
                        else
                        {
                            canvas.OpenStorageMenu((Storage)interactable);
                        }
                        isInMenu = !isInMenu;
                        break;
					case InteractableObject.Welder:
                        isInMenu = !isInMenu;
                        // Welder
                        break;
                }
            }
        }
    }

}
