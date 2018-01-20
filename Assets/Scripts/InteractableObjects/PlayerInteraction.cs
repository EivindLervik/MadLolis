using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

    public CanvasScript canvas;

    private bool isInMenu;
    private Interactable interactable;
    private NPC npc;

    private CharacterInput characterInput;

    private void Start()
    {
        characterInput = GetComponent<CharacterInput>();
        canvas.SetPlayerInteraction(this);
        canvas.SetCharacterInput(characterInput);
        canvas.SetPlayer(GetComponent<Character>());

        Cursor.visible = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        string hitTag = other.transform.tag;

        switch (hitTag)
        {
            case "Interactable":
                interactable = other.GetComponent<Interactable>();
                canvas.OpenInteraction(interactable.interactableType + " " + interactable.objectName, interactable.activationKey);
                break;
            case "Talkable":
                npc = other.GetComponent<NPC>();
                canvas.OpenInteraction("Talk to " + npc.characterName, KeyCode.E);
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canvas.HideInteraction();
        interactable = null;
        npc = null;
    }

    private void Update()
    {
        if (npc != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isInMenu)
                {
                    canvas.CloseDialogueMenu();
                }
                else
                {
                    canvas.OpenDialogueMenu(npc);
                }
                isInMenu = !isInMenu;
            }
        }
        if (interactable != null)
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
                    case InteractableObject.Arcade:
                        isInMenu = !isInMenu;
                        SceneHandler.GoToScene(((Playable)interactable).sceneChange);
                        break;
                }
            }
        }
    }

    public void EnablePlayerInteraction()
    {
        isInMenu = false;
    }

}
