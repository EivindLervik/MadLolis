using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : Interactable {

	public List<StorageEntry> items;

}

[System.Serializable]
public class StorageEntry{
	public DataHandler.InGameObject item;
	public int quantity;
}
