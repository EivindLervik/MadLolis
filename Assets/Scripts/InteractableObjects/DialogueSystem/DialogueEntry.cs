using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DialogueEntryType{
	Text, Fork, Choice
}
	
[System.Serializable]
public class DialogueEntry : MonoBehaviour{

	[Header("Dialogue Entry")]
	public string dialogueTag;
	public DialogueEntryType dialogueEntryType;

	// TEXT
	public string nextDialogueTag;
	public string dialogueText;
	public AudioClip voice;

	// FORK
	public string nextDialogueTag_Success;
	public string nextDialogueTag_Failure;
	public DialogueRequirement[] forkRequirements;

	// CHOICE
	public List<DialogueOption> dialogueOptions;

}
