using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class DataHandler : MonoBehaviour {

	public enum InGameObject{
        // Ores
        Iron_Ore
		,Coal_Ore
		,Silver_Ore
		,Gold_Ore
		,Diamond_Ore

        // Wood
        ,Birch_Logs
        ,Oak_Logs
        ,Spurce_Logs

        // Animals
        ,Boar_Hide
        ,Boar_Meat
        
        // ScrapMetal
        ,CircuitBorad
    }

    public enum InGameObjectType{
		Resource, Weapon
	}

	public List<StorageItem> items_Ores;
    public List<StorageItem> items_Logs;
    public List<StorageItem> items_Animals;
    public List<StorageItem> items_Scrap;
    public List<AmmoType> ammo;

    private static Dictionary<InGameObject, StorageItem> itemsDict;
    private static Dictionary<string, AmmoType> ammoDict;
    private static string playerName;

    private void Awake()
    {
        itemsDict = new Dictionary<InGameObject, StorageItem>();
        ammoDict = new Dictionary<string, AmmoType>();

        foreach(StorageItem si in items_Ores)
        {
            itemsDict.Add(si.inGameObject, si);
        }
        foreach (StorageItem si in items_Logs)
        {
            itemsDict.Add(si.inGameObject, si);
        }
        foreach (StorageItem si in items_Animals)
        {
            itemsDict.Add(si.inGameObject, si);
        }
        foreach (StorageItem si in items_Scrap)
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
