using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueTree : MonoBehaviour {

    [Header("Dialogue Tree")]
    public string firstTag = "entry";
	private Dictionary<string, DialogueEntry> dialogueEntries;
    protected NPC npc;

    public void Setup(NPC npc, DialogueEntry[] dialogueEntries)
    {
        this.npc = npc;
        this.dialogueEntries = new Dictionary<string, DialogueEntry>();

        foreach(DialogueEntry de in dialogueEntries)
        {
            de.Setup(npc);
            this.dialogueEntries.Add(de.dialogueTag, de);
        }
    }

    public DialogueEntry StartDialogue(Character player)
    {
        foreach (KeyValuePair<string, DialogueEntry> kv in dialogueEntries)
        {
            kv.Value.LinkPlayer(player);
        }

        return dialogueEntries[firstTag];
    }

    public DialogueEntry Proceed(DialogueEntry de)
    {
        string key = "exit";

        if(de.GetDialogueEntryType() == DialogueEntryType.Text)
        {
            key = ((DialogueText)de).nextDialogueTag;
        }
        else if (de.GetDialogueEntryType() == DialogueEntryType.Fork)
        {
            key = ((DialogueFork)de).EvaluateFork();
        }

        if (key.Equals("exit"))
        {
            return null;
        }
        else
        {
            return dialogueEntries[key];
        }
    }

    public DialogueEntry ChooseCoice(DialogueOption option)
    {
        string key = "exit";

        key = option.nextDialogueTag;

        if (key.Equals("exit"))
        {
            return null;
        }
        else
        {
            return dialogueEntries[key];
        }
    }

}
