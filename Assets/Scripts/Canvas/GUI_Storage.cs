using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_Storage : GUI_Element {

    [Header("Storage")]
    public Storage storage_PlayerStorage;
    private Storage storage_StorageStorage;
    private Dictionary<DataHandler.InGameObject, StorageListItem> storage_StorageListOfEntries;
    private Dictionary<DataHandler.InGameObject, StorageListItem> storage_PlayerListOfEntries;
    public Text storage_Name;
    public Text storage_PlayerName;
    public Text storage_StorageCapacity;
    public Text storage_PlayerCapacity;
    public RectTransform storage_StorageInventory;
    public RectTransform storage_PlayerInventory;
    public RectTransform storage_StorageEntry;
    public RectTransform storage_PlayerEntry;
    public float storage_ItemsPerPage;
    public float storage_ItemsSpace;
    public float storage_ItemsTopSpace;
    public float storage_ItemsBottomSpace;
    public GameObject storage_Item;

    private void Awake()
    {
        storage_StorageListOfEntries = new Dictionary<DataHandler.InGameObject, StorageListItem>();
        storage_PlayerListOfEntries = new Dictionary<DataHandler.InGameObject, StorageListItem>();
    }

    private void Update()
    {
        UpdateStorageCapacity();
    }

    public void UpdateStorageCapacity()
    {
        storage_StorageCapacity.text = storage_StorageStorage.currentWeight + "kg / " + storage_StorageStorage.maxWeight + "kg";
        storage_PlayerCapacity.text = storage_PlayerStorage.currentWeight + "kg / " + storage_PlayerStorage.maxWeight + "kg";
    }

    public void OpenStorageMenu(Storage storage, Character player)
    {
        storage_Name.text = storage.objectName;
        storage_PlayerName.text = player.characterName;

        storage_StorageStorage = storage;
        UpdateStorageCapacity();

        foreach (KeyValuePair<DataHandler.InGameObject, StorageListItem> kvp in storage_StorageListOfEntries)
        {
            Destroy(kvp.Value.gameObject);
        }
        foreach (KeyValuePair<DataHandler.InGameObject, StorageListItem> kvp in storage_PlayerListOfEntries)
        {
            Destroy(kvp.Value.gameObject);
        }

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
        if (me == storage_StorageInventory)
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
}
