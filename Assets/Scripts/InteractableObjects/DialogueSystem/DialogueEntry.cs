using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DialogueEntryType{
	Text, Fork, Choice
}
	
[System.Serializable]
public class DialogueEntry : MonoBehaviour {

	[Header("Dialogue Entry")]
	public string dialogueTag;
	protected DialogueEntryType dialogueEntryType;
    protected NPC npc;
    protected Character player;

    public void Setup(NPC npc)
    {
        this.npc = npc;
    }

    public DialogueEntryType GetDialogueEntryType()
    {
        return dialogueEntryType;
    }

    public void LinkPlayer(Character player)
    {
        this.player = player;
    }

}
