using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour {

	public enum InGameObject{
		// Ores
		Iron_Ore
		,Coal_Ore
		,Silver_Ore
		,Gold_Ore
		,Diamond_Ore

		// Items

	}

	public enum InGameObjectType{
		Resource, Weapon,
	}

	public List<StorageItem> items;

    private static Dictionary<InGameObject, StorageItem> itemsDict;
    private static string playerName;

    private void Awake()
    {
        itemsDict = new Dictionary<InGameObject, StorageItem>();

        foreach(StorageItem si in items)
        {
            itemsDict.Add(si.inGameObject, si);
        }

        playerName = "Eivind";
    }

    public static StorageItem GetStorageItem(InGameObject igo)
    {
        return itemsDict[igo];
    }

    public static string GetPlayerName()
    {
        return playerName;
    }

}
