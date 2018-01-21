using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Custom/Storage Item")]
public class StorageItem : ScriptableObject {

	public DataHandler.InGameObject inGameObject;
	public DataHandler.InGameObjectType inGameObjectType;
	public string itemName;
    public float weight;
	public string description;
	public Sprite icon;
	public GameObject itemObject;

}
