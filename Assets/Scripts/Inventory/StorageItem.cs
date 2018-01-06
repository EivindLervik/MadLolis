using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StorageItem {

	public DataHandler.InGameObject inGameObject;
	public DataHandler.InGameObjectType inGameObjectType;
	public string itemName;
    public float weight;
	public string description;
	public Sprite icon;
	public GameObject itemObject;

}
