using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueOption : ScriptableObject {

	public string nextDialogueTag;
	public string optionText;
	public List<DialogueRequirement> requirements;
	public List<DialogueEffect> effects;
}
