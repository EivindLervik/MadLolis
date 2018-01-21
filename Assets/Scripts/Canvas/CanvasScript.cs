using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour {

    public GameObject crosshairObject;
    public GameObject interactionObject;
    public GameObject dialogueObject;
    public GameObject airControlsObject;
    public GameObject storageObject;
    public GameObject carObject;

    [Header("Lathe")]
    public GameObject lathe_MENU;

    private CharacterInput characterInput;
    private PlayerInteraction playerInteraction;
    private Character player;

    // NEW METHOD OF UPDATING
    private GUI_Crosshair crosshair;
    private GUI_AirControls airControls;
    private GUI_Car car;
    private GUI_Storage storage;
    private GUI_Interaction interaction;
    private GUI_Dialogue dialogue;

    private void Awake()
    {
        GetAirControls();
        GetCrosshair();
        GetSpeedometer();
        GetStorage();
        GetInteraction();
        GetDialogue();

        // Setup Start
        ShowCrosshair();
    }



    /**
     * PUBLIC
    **/
    public void SetCharacterInput(CharacterInput characterInput)
    {
        this.characterInput = characterInput;
    }

    public void SetPlayerInteraction(PlayerInteraction playerInteraction)
    {
        this.playerInteraction = playerInteraction;
    }

    public void SetPlayer(Character player)
    {
        this.player = player;
    }



    /**
     * PRIVATE
    **/
    private void AllowInput()
    {
        if (characterInput != null)
        {
            characterInput.AllowInput(true);
        }
    }

    private void FreezePlayer()
    {
        characterInput.AllowInput(false);
        HideCrosshair();
        HideInteraction();
    }



    /**
     * SETUPS
    **/
    #region Crosshair
    private void GetCrosshair()
    {
        crosshair = crosshairObject.GetComponentInChildren<GUI_Crosshair>();
    }
    private void ShowCrosshair()
    {
        if (crosshair)
        {
            GetCrosshair();
        }
        crosshairObject.SetActive(true);
    }
    public void UpdateCrosshair(List<string> data)
    {
        crosshair.UpdateGUI(data);
    }
    private void HideCrosshair()
    {
        if (crosshair)
        {
            crosshair.Hide();
        }
        else
        {
            crosshairObject.SetActive(false);
        }
    }
    #endregion

    #region AirControls
    private void GetAirControls()
    {
        airControls = airControlsObject.GetComponentInChildren<GUI_AirControls>();
    }
    public void ShowAirControls()
    {
        if (airControls)
        {
            GetAirControls();
        }
        airControlsObject.SetActive(true);
    }
    public void UpdateAirControls(List<string> data)
    {
        airControls.UpdateGUI(data);
    }
    public void HideAirControls()
    {
        if (airControls)
        {
            airControls.Hide();
        }
        else
        {
            airControlsObject.SetActive(false);
        }
    }
    #endregion

    #region Speedometer
    private void GetSpeedometer()
    {
        car = carObject.GetComponentInChildren<GUI_Car>();
    }
    public void ShowSpeedometer()
    {
        if (car)
        {
            GetSpeedometer();
        }
        carObject.SetActive(true);
    }
    public void UpdateSpeedometer(List<string> data)
    {
        car.UpdateGUI(data);
    }
    public void HideSpeedometer()
    {
        if (car)
        {
            car.Hide();
        }
        else
        {
            carObject.SetActive(false);
        }
    }
    
    #endregion

    #region Lathe
    public void OpenLatheMenu()
    {
        
    }
    public void CloseLatheMenu()
    {
        
    }
    #endregion

    #region Storage
    private void GetStorage()
    {
        storage = storageObject.GetComponentInChildren<GUI_Storage>();
    }
    public void OpenStorageMenu(Storage storage)
	{
        Cursor.visible = true;
        FreezePlayer();
        HideCrosshair();

        if (!storageObject)
        {
            GetSpeedometer();
        }
        storageObject.SetActive(true);

        this.storage.OpenStorageMenu(storage, player);
    }
    public void CloseStorageMenu()
    {
        Cursor.visible = false;
        AllowInput();
        interactionObject.SetActive(true);

        if (storageObject)
        {
            storage.Hide();
        }
        else
        {
            storageObject.SetActive(false);
        }
    }
    #endregion

    #region Dialogue
    private void GetDialogue()
    {
        dialogue = dialogueObject.GetComponentInChildren<GUI_Dialogue>();
    }
    public void OpenDialogueMenu(NPC npc)
    {
        Cursor.visible = true;
        FreezePlayer();
        HideCrosshair();
        
        if (crosshair)
        {
            GetDialogue();
        }
        dialogueObject.SetActive(true);

        dialogue.OpenDialogueMenu(npc, this, player);
    }
    public void CloseDialogueMenu()
    {
        Cursor.visible = false;
        AllowInput();
        ShowCrosshair();

        interactionObject.SetActive(true);
        playerInteraction.EnablePlayerInteraction();

        dialogue.CloseDialogueMenu();
        if (storageObject)
        {
            dialogue.Hide();
        }
        else
        {
            dialogueObject.SetActive(false);
        }
    }
    #endregion

    #region Interaction
    private void GetInteraction()
    {
        interaction = interactionObject.GetComponentInChildren<GUI_Interaction>();
    }
    public void OpenInteraction(string text, KeyCode key)
    {
        if (car)
        {
            GetInteraction();
        }
        interactionObject.SetActive(true);
        interaction.OpenInteraction(text, key);
    }
    public void HideInteraction()
    {
        ShowCrosshair();

        if (interactionObject)
        {
            interaction.Hide();
        }
        else
        {
            interactionObject.SetActive(false);
        }
    }
#endregion



}
