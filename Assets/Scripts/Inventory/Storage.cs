using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : Interactable {

    public float maxWeight;
    public float currentWeight;
	public List<StorageEntry> items;

    private void Start()
    {
        foreach(StorageEntry se in items)
        {
            currentWeight += se.quantity * DataHandler.GetStorageItem(se.item).weight;
        }
    }

    // 0 is failed
    // 1 is new entry
    // 2 is add quantity
    public int Add(StorageEntry se)
    {
        bool exist = false;
        int index = 0;
        foreach (StorageEntry sei in items)
        {
            //print(sei.item);
            if (sei.item == se.item)
            {
                exist = true;
                break;
            }
            else
            {
                index++;
            }
        }

        if (exist)
        {
            float addedWeight = DataHandler.GetStorageItem(se.item).weight;
            if (currentWeight + (se.quantity * addedWeight) <= maxWeight)
            {
                items[index].quantity += se.quantity;
                currentWeight += se.quantity * addedWeight;
                return 2;
            }
            return 0;
        }
        else
        {
            float addedWeight = DataHandler.GetStorageItem(se.item).weight;
            if (currentWeight + (se.quantity * addedWeight) <= maxWeight)
            {
                items.Add(se);
                currentWeight += se.quantity * DataHandler.GetStorageItem(se.item).weight;
                return 1;
            }
            return 0;
        }
    }

    public int Remove(DataHandler.InGameObject ob, int quantity)
    {
        bool exist = false;
        int index = 0;

        foreach (StorageEntry sei in items)
        {
            if (sei.item == ob)
            {
                exist = true;
                break;
            }
            else
            {
                index++;
            }
        }

        int ret = 0;
        if (exist)
        {
            ret = Remove(index, quantity);
        }
        return ret;
    }

    private int Remove(int index, int quantity)
    {
        int action = 0;
        StorageEntry se = items[index];

        if (se.quantity - quantity > 0)
        {
            se.quantity -= quantity;
            currentWeight -= quantity * DataHandler.GetStorageItem(se.item).weight;
            action = 1;
        }
        else if (se.quantity - quantity == 0)
        {
            currentWeight -= quantity * DataHandler.GetStorageItem(se.item).weight;
            items.RemoveAt(index);
            action = 2;
        }

        return action;
    }

    public StorageEntry GetStorageEntry(StorageItem si)
    {
        foreach (StorageEntry sei in items)
        {
            //print(sei.item);
            if (sei.item == si.inGameObject)
            {
                return sei;
            }
        }
        return null;
    }

    public int GetStoragePosition(DataHandler.InGameObject ob)
    {
        int index = -1;

        foreach (StorageEntry sei in items)
        {
            if (sei.item == ob)
            {
                break;
            }
            else
            {
                index++;
            }
        }

        return index;
    }

}

[System.Serializable]
public class StorageEntry{
	public DataHandler.InGameObject item;
	public int quantity;

    public StorageEntry(DataHandler.InGameObject item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
}
