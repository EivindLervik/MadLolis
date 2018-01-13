using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueOption {

	public string nextDialogueTag;
	public string optionText;
	public List<DialogueRequirement> requirements;

    public bool Check(NPC npc, Character player)
    {
        foreach(DialogueRequirement dr in requirements)
        {
            if(!dr.Check(npc, player))
            {
                return false;
            }
        }
        return true;
    }
}
