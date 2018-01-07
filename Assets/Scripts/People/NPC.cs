using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Character {

	[Header("NPC")]
	public float affection;
	public float liking;

    private DialogueTree dialogueTree;

    private void Start()
    {
        dialogueTree = GetComponentInChildren<DialogueTree>();
        dialogueTree.Setup(this, GetComponentsInChildren<DialogueEntry>());
    }

    public DialogueTree StartDialogue(Character player, out DialogueEntry de)
    {
        de = dialogueTree.StartDialogue(player);
        return dialogueTree;
    }

}
