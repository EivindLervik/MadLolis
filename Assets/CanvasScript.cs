using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour {

    [Header("Crosshair")]
    public RectTransform up;
    public RectTransform down;
    public RectTransform left;
    public RectTransform right;

    [Header("Interaction")]
    public GameObject interaction_MENU;
    public Text interaction_Text;
    public Text interaction_Key;

    [Header("Dialogue")]
    public GameObject dialogue_MENU;
    public Text dialogue_Header;
    public Text dialogue_Text;
    public RectTransform dialogue_Choices;
    public RectTransform dialogue_DialogueEntry;
    public RectTransform dialogue_DialogueInventory;
    private List<DialogueListItem> dialogue_DialogueListOfEntries;
    public float dialogue_ItemsPerPage;
    public float dialogue_ItemsSpace;
    public float dialogue_ItemsTopSpace;
    public float dialogue_ItemsBottomSpace;
    public GameObject dialogue_Item;
    public KeyCode continueKey;
    private DialogueTree dialogueTree;
    private DialogueEntry currentEntry;

    [Header("Lathe")]
    public GameObject lathe_MENU;

	[Header("Storage")]
	public GameObject storage_MENU;
    public Storage storage_PlayerStorage;
    private Storage storage_StorageStorage;
    private Dictionary<DataHandler.InGameObject, StorageListItem> storage_StorageListOfEntries;
    private Dictionary<DataHandler.InGameObject, StorageListItem> storage_PlayerListOfEntries;
    public Text storage_Name;
    public Text storage_PlayerName;
    public RectTransform storage_StorageInventory;
    public RectTransform storage_PlayerInventory;
    public RectTransform storage_StorageEntry;
    public RectTransform storage_PlayerEntry;
    public float storage_ItemsPerPage;
    public float storage_ItemsSpace;
    public float storage_ItemsTopSpace;
    public float storage_ItemsBottomSpace;
    public GameObject storage_Item;

    [Header("Speedometer")]
    public GameObject speedometer;
    public RectTransform speedometerNeedle;

    private Vector3 up_SP;
    private Vector3 down_SP;
    private Vector3 left_SP;
    private Vector3 right_SP;

    private float fadeback;
    private CharacterInput characterInput;
    private PlayerInteraction playerInteraction;
    private Character player;

    private void Start()
    {
        if(up != null && down != null && left != null && right != null)
        {
            up_SP = up.position;
            down_SP = down.position;
            left_SP = left.position;
            right_SP = right.position;
        }

        CLoseAllMenues();
    }

    private void Update()
    {
        if(up != null && down != null && left != null && right != null)
        {
            if (up.position != up_SP)
            {
                up.position = Vector3.Lerp(up.position, up_SP, Time.deltaTime * fadeback);
            }
            if (down.position != down_SP)
            {
                down.position = Vector3.Lerp(down.position, down_SP, Time.deltaTime * fadeback);
            }
            if (left.position != left_SP)
            {
                left.position = Vector3.Lerp(left.position, left_SP, Time.deltaTime * fadeback);
            }
            if (right.position != right_SP)
            {
                right.position = Vector3.Lerp(right.position, right_SP, Time.deltaTime * fadeback);
            }
        }

        if(dialogueTree != null && currentEntry != null)
        {
            if (Input.GetKeyDown(continueKey))
            {
                ContinueDialogue();
            }
        }
        
    }

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

    #region Speedometer
    public void ShowSpeedometer()
    {
        speedometer.SetActive(true);
    }
    public void HideSpeedometer()
    {
        speedometer.SetActive(false);
    }
    public void UpdateSpeedometer(float speed)
    {
        float index = speed / 180.0f;
        speedometerNeedle.localEulerAngles = new Vector3(0.0f, 0.0f, (index * - 180.0f) + 90.0f);
    }
    #endregion

#region Lathe
    public void OpenLatheMenu()
    {
        Cursor.visible = true;
        FreezePlayer ();
        lathe_MENU.SetActive(true);
    }
    public void CloseLatheMenu()
    {
        Cursor.visible = false;
        AllowInput();
        ShowCrosshair();
        interaction_MENU.SetActive(true);
        CloseMenu(lathe_MENU);
    }
    #endregion

#region Storage
    public void OpenStorageMenu(Storage storage)
	{
        Cursor.visible = true;
        FreezePlayer ();
		storage_MENU.SetActive(true);
        storage_Name.text = storage.objectName;
        storage_PlayerName.text = player.characterName;

        storage_StorageStorage = storage;

        storage_StorageListOfEntries = new Dictionary<DataHandler.InGameObject, StorageListItem>();
        storage_PlayerListOfEntries = new Dictionary<DataHandler.InGameObject, StorageListItem>();

        SetupScrollList(storage, storage_StorageInventory, storage_StorageEntry, storage_StorageListOfEntries);
        SetupScrollList(storage_PlayerStorage, storage_PlayerInventory, storage_PlayerEntry, storage_PlayerListOfEntries);
    }
    private void SetupScrollList(Storage storage, RectTransform storage_inventory, RectTransform entry, Dictionary<DataHandler.InGameObject, StorageListItem> storage_ListOfEntries)
    {
        float storage_frameHeight = storage_inventory.rect.height;
        float storage_itemHeight = storage_frameHeight / storage_ItemsPerPage;
        float storage_itemSpace = storage_itemHeight / storage_ItemsSpace;

        entry.sizeDelta = new Vector2(entry.sizeDelta.x, (storage.items.Count * (storage_itemHeight + storage_itemSpace)) + storage_ItemsBottomSpace + storage_ItemsTopSpace);

        int index = 0;
        foreach (StorageEntry se in storage.items)
        {
            StorageListItem sli = SetupScrollListItem(se, entry, storage_inventory, storage_itemHeight, storage_itemSpace, index);
            storage_ListOfEntries.Add(sli.GetStorageItem().inGameObject, sli);
            index++;
        }
    }
    private StorageListItem SetupScrollListItem(StorageEntry se, RectTransform entry, RectTransform storage_inventory, float storage_itemHeight, float storage_itemSpace, int index)
    {
        StorageItem si = DataHandler.GetStorageItem(se.item);
        int amount = se.quantity;

        StorageListItem sli = Instantiate(storage_Item, entry).GetComponent<StorageListItem>();
        RectTransform itemRect = sli.GetComponent<RectTransform>();

        Vector2 itemRectSize = itemRect.sizeDelta;
        itemRectSize.y = storage_itemHeight;
        itemRect.sizeDelta = itemRectSize;

        Vector3 itemRectPos = itemRect.localPosition;
        itemRectPos.y -= storage_ItemsTopSpace + (index * (storage_itemSpace + storage_itemHeight));
        itemRect.localPosition = itemRectPos;

        sli.Populate(this, storage_inventory, si, amount, StorageListItemClickAction.Move);

        return sli;
    }
    public void MoveItemToOtherThan(RectTransform me, StorageListItem sli)
    {
        if(me == storage_StorageInventory)
        {
            int action = RemoveFromMe_Data(sli, storage_StorageInventory, storage_StorageEntry, storage_StorageStorage, storage_StorageListOfEntries);
            if (action == 0)
            {
                Debug.LogWarning("Failed to remove");
            }
            else if (action == 1)
            {
                MoveItemToOther(sli, storage_PlayerInventory, storage_PlayerEntry, storage_PlayerStorage, storage_PlayerListOfEntries);
                RemoveFromMe_Graphics(true, sli, storage_StorageInventory, storage_StorageEntry, storage_StorageStorage, storage_StorageListOfEntries);
            }
            else if (action == 2)
            {
                MoveItemToOther(sli, storage_PlayerInventory, storage_PlayerEntry, storage_PlayerStorage, storage_PlayerListOfEntries);
                RemoveFromMe_Graphics(false, sli, storage_StorageInventory, storage_StorageEntry, storage_StorageStorage, storage_StorageListOfEntries);
            }
        }
        else if (me == storage_PlayerInventory)
        {
            int action = RemoveFromMe_Data(sli, storage_PlayerInventory, storage_PlayerEntry, storage_PlayerStorage, storage_PlayerListOfEntries);
            if (action == 0)
            {
                Debug.LogWarning("Failed to remove");
            }
            else if (action == 1)
            {
                MoveItemToOther(sli, storage_StorageInventory, storage_StorageEntry, storage_StorageStorage, storage_StorageListOfEntries);
                RemoveFromMe_Graphics(true, sli, storage_PlayerInventory, storage_PlayerEntry, storage_PlayerStorage, storage_PlayerListOfEntries);
            }
            else if (action == 2)
            {
                MoveItemToOther(sli, storage_StorageInventory, storage_StorageEntry, storage_StorageStorage, storage_StorageListOfEntries);
                RemoveFromMe_Graphics(false, sli, storage_PlayerInventory, storage_PlayerEntry, storage_PlayerStorage, storage_PlayerListOfEntries);
            }
        }
    }
    private void MoveItemToOther(StorageListItem sli, RectTransform storage_inventory, RectTransform storage_entry, Storage storage_storage, Dictionary<DataHandler.InGameObject, StorageListItem> storage_ListOfEntries)
    {
        float storage_frameHeight = storage_inventory.rect.height;
        float storage_itemHeight = storage_frameHeight / storage_ItemsPerPage;
        float storage_itemSpace = storage_itemHeight / storage_ItemsSpace;

        StorageEntry se;
        if (Input.GetButton("Run"))
        {
            se = new StorageEntry(sli.GetStorageItem().inGameObject, int.Parse(sli.amount.text));
        }
        else
        {
            se = new StorageEntry(sli.GetStorageItem().inGameObject, 1);
        }

        int result = storage_storage.Add(se);

        if (result == 0) // Failed
        {
            Debug.LogWarning("Failed");
        }
        else if (result == 1) // New entry
        {
            int entries = storage_storage.items.Count;
            storage_entry.sizeDelta = new Vector2(storage_entry.sizeDelta.x, (entries * (storage_itemHeight + storage_itemSpace)) + storage_ItemsBottomSpace + storage_ItemsTopSpace);
            StorageListItem sliNew = SetupScrollListItem(se, storage_entry, storage_inventory, storage_itemHeight, storage_itemSpace, entries - 1);
            storage_ListOfEntries.Add(sliNew.GetStorageItem().inGameObject, sliNew);
        }
        else if (result == 2) // Add quantity
        {
            StorageEntry seNew = storage_storage.GetStorageEntry(DataHandler.GetStorageItem(se.item));
            storage_ListOfEntries[se.item].UpdateAmount(seNew.quantity, DataHandler.GetStorageItem(seNew.item));
        }
    }
    private int RemoveFromMe_Data(StorageListItem sli, RectTransform storage_inventory, RectTransform storage_entry, Storage storage_storage, Dictionary<DataHandler.InGameObject, StorageListItem> storage_ListOfEntries)
    {
        float storage_frameHeight = storage_inventory.rect.height;
        float storage_itemHeight = storage_frameHeight / storage_ItemsPerPage;
        float storage_itemSpace = storage_itemHeight / storage_ItemsSpace;

        int result;
        if (Input.GetButton("Run"))
        {
            result = storage_storage.Remove(sli.GetStorageItem().inGameObject, int.Parse(sli.amount.text));
        }
        else
        {
            result = storage_storage.Remove(sli.GetStorageItem().inGameObject, 1);
        }

        return result;
    }
    private void RemoveFromMe_Graphics(bool update, StorageListItem sli, RectTransform storage_inventory, RectTransform storage_entry, Storage storage_storage, Dictionary<DataHandler.InGameObject, StorageListItem> storage_ListOfEntries)
    {
        if (update)
        {
            StorageEntry seNew = storage_storage.GetStorageEntry(DataHandler.GetStorageItem(sli.GetStorageItem().inGameObject));
            storage_ListOfEntries[sli.GetStorageItem().inGameObject].UpdateAmount(seNew.quantity, DataHandler.GetStorageItem(sli.GetStorageItem().inGameObject));
        }
        else
        {
            
            float storage_frameHeight = storage_inventory.rect.height;
            float storage_itemHeight = storage_frameHeight / storage_ItemsPerPage;
            float storage_itemSpace = storage_itemHeight / storage_ItemsSpace;

            DataHandler.InGameObject igo = sli.GetStorageItem().inGameObject;

            int entries = storage_storage.items.Count;
            int listIndex = storage_storage.GetStoragePosition(igo);

            // Move all others under the item up one notch
            for (int i = 0; i < storage_entry.childCount; i++)
            {
                Transform rt = storage_entry.GetChild(i);
                if (rt.position.y < storage_ListOfEntries[igo].transform.position.y)
                {
                    rt.Translate(new Vector3(0.0f, storage_itemHeight + storage_itemSpace, 0.0f));
                }
            }

            storage_entry.sizeDelta = new Vector2(storage_entry.sizeDelta.x, (entries * (storage_itemHeight + storage_itemSpace)) + storage_ItemsBottomSpace + storage_ItemsTopSpace);
            Destroy(storage_ListOfEntries[igo].gameObject);
            storage_ListOfEntries.Remove(igo);
            
        }
    }
    public void CloseStorageMenu()
    {
        Cursor.visible = false;
        AllowInput();
        ShowCrosshair();
        interaction_MENU.SetActive(true);
        CloseMenu(storage_MENU);
    }
    #endregion

#region Dialogue
    public void OpenDialogueMenu(NPC npc)
    {
        Cursor.visible = true;
        FreezePlayer();
        dialogue_MENU.SetActive(true);

        DialogueEntry de;
        dialogueTree = npc.StartDialogue(player, out de);
        currentEntry = de;

        dialogue_Header.text = npc.characterName;
        DisplayDialogueEntry();
}
    private void ContinueDialogue()
    {
        currentEntry = dialogueTree.Proceed(currentEntry);
        DialogueIsEnd();
    }
    public void ChooseOption(DialogueOption option)
    {
        currentEntry = dialogueTree.ChooseCoice(option);
        DialogueIsEnd();
    }
    private void DialogueIsEnd()
    {
        if (currentEntry == null)
        {
            CloseDialogueMenu();
        }
        else
        {
            DisplayDialogueEntry();
        }
    }
    private void DisplayDialogueEntry()
    {
        if(currentEntry.GetDialogueEntryType() == DialogueEntryType.Text)
        {
            dialogue_Text.gameObject.SetActive(true);
            dialogue_Choices.gameObject.SetActive(false);

            DialogueText dt = (DialogueText)currentEntry;
            dialogue_Text.text = dt.dialogueText;
        }
        else if (currentEntry.GetDialogueEntryType() == DialogueEntryType.Fork)
        {
            dialogue_Text.gameObject.SetActive(false);
            dialogue_Choices.gameObject.SetActive(false);

            ContinueDialogue();
        }
        else if (currentEntry.GetDialogueEntryType() == DialogueEntryType.Choice)
        {
            dialogue_Text.gameObject.SetActive(false);
            dialogue_Choices.gameObject.SetActive(true);

            float dialogue_frameHeight = dialogue_DialogueInventory.rect.height;
            float dialogue_itemHeight = dialogue_frameHeight / dialogue_ItemsPerPage;
            float dialogue_itemSpace = dialogue_itemHeight / dialogue_ItemsSpace;

            dialogue_DialogueListOfEntries = new List<DialogueListItem>();

            DialogueChoice dc = (DialogueChoice)currentEntry;
            dialogue_DialogueEntry.sizeDelta = new Vector2(dialogue_DialogueEntry.sizeDelta.x, (dc.dialogueOptions.Count * (dialogue_itemHeight + dialogue_itemSpace)) + dialogue_ItemsBottomSpace + dialogue_ItemsTopSpace);

            int index = 0;
            foreach (DialogueOption option in dc.dialogueOptions)
            {
                DialogueListItem dli = Instantiate(dialogue_Item, dialogue_DialogueEntry).GetComponent<DialogueListItem>();
                RectTransform itemRect = dli.GetComponent<RectTransform>();

                dialogue_DialogueListOfEntries.Add(dli);

                Vector2 itemRectSize = itemRect.sizeDelta;
                itemRectSize.y = dialogue_itemHeight;
                itemRect.sizeDelta = itemRectSize;

                Vector3 itemRectPos = itemRect.localPosition;
                itemRectPos.y -= dialogue_ItemsTopSpace + (index * (dialogue_itemSpace + dialogue_itemHeight));
                itemRect.localPosition = itemRectPos;

                dli.Populate(this, option, dc, option.optionText);

                index++;
            }
        }
    }
    public void CloseDialogueMenu()
    {
        Cursor.visible = false;
        AllowInput();
        ShowCrosshair();
        interaction_MENU.SetActive(true);
        playerInteraction.EnablePlayerInteraction();
        CloseMenu(dialogue_MENU);

        currentEntry = null;

        dialogueTree = null;
        currentEntry = null;
    }
#endregion

    #region Interaction
    public void PromptInteraction(string text, KeyCode key)
    {
        interaction_MENU.SetActive(true);
        interaction_Text.text = text;
        interaction_Key.text = key.ToString();
    }

    public void HideInteraction()
    {
        interaction_MENU.SetActive(false);
    }
#endregion


    public void CrosshairEffect(float magnitude, float fadeback)
    {
        this.fadeback = fadeback;

        float magnitudeModifier = 25.0f;
        Vector3 effect = Vector3.up * magnitude * magnitudeModifier;

        up.Translate(effect);
        down.Translate(effect);
        left.Translate(effect);
        right.Translate(effect);
    }

    private void CLoseAllMenues()
    {
        Cursor.visible = false;
        CloseMenu(interaction_MENU);
        CloseMenu(lathe_MENU);
        CloseMenu(storage_MENU);
        CloseMenu(dialogue_MENU);
        //CloseMenu(speedometer);

        AllowInput();
    }

    private void AllowInput()
    {
        if (characterInput != null)
        {
            characterInput.AllowInput(true);
        }
    }

    private void CloseMenu(GameObject go)
    {
        if(go != null)
        {
            go.SetActive(false);
        }
    }

    private void HideCrosshair()
    {
        up.gameObject.SetActive(false);
        down.gameObject.SetActive(false);
        left.gameObject.SetActive(false);
        right.gameObject.SetActive(false);
    }

    private void ShowCrosshair()
    {
        up.gameObject.SetActive(true);
        down.gameObject.SetActive(true);
        left.gameObject.SetActive(true);
        right.gameObject.SetActive(true);
    }

	private void FreezePlayer(){
		characterInput.AllowInput(false);
		HideCrosshair();
		HideInteraction();
	}
}
