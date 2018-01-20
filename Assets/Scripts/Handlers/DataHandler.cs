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
    public List<AmmoType> ammo;

    private static Dictionary<InGameObject, StorageItem> itemsDict;
    private static Dictionary<string, AmmoType> ammoDict;
    private static string playerName;

    private void Awake()
    {
        itemsDict = new Dictionary<InGameObject, StorageItem>();
        ammoDict = new Dictionary<string, AmmoType>();

        foreach(StorageItem si in items)
        {
            itemsDict.Add(si.inGameObject, si);
        }

        foreach (AmmoType at in ammo)
        {
            ammoDict.Add(at.ammoName, at);
        }

        playerName = "Eivind";
    }

    public static StorageItem GetStorageItem(InGameObject igo)
    {
        return itemsDict[igo];
    }

    public static AmmoType GetAmmoType(string at)
    {
        return ammoDict[at];
    }

    public static string GetPlayerName()
    {
        return playerName;
    }

}
