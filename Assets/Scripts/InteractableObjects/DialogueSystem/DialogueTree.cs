using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueTree : MonoBehaviour {

	[Header("Dialogue Tree")]
	public string firstTag;
	private DialogueEntry[] dialogueEntries;

	private void Start(){
		dialogueEntries = GetComponentsInChildren <DialogueEntry> ();
	}

}
