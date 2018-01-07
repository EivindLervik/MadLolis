using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueFork : DialogueEntry {

    [Header("Dialogue Fork")]
    public string nextDialogueTag_Success;
    public string nextDialogueTag_Failure;
    public DialogueRequirement[] forkRequirements;

    private void Start()
    {
        dialogueEntryType = DialogueEntryType.Fork;
    }

    public string EvaluateFork()
    {
        bool success = true;

        foreach(DialogueRequirement dr in forkRequirements)
        {

            if (!dr.Check(npc, player))
            {
                success = false;
                break;
            }
        }

        if (success)
        {
            return nextDialogueTag_Success;
        }
        else
        {
            return nextDialogueTag_Failure;
        }
    }
}
