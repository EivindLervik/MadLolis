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
		,Diamond

		// Items

	}

	public enum InGameObjectType{
		Resource, Weapon,
	}

	public List<StorageItem> items;

}
