using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DialogueEntry))]
public class DialogueEntryEditor : Editor
{

	public override void OnInspectorGUI()
	{
		DialogueEntry script = (DialogueEntry)target;

		script.dialogueEntryType = (DialogueEntryType)EditorGUILayout.EnumPopup("Dialogue Type", script.dialogueEntryType);

		if (script.dialogueEntryType == DialogueEntryType.Text)
		{
			script.nextDialogueTag = EditorGUILayout.TextField("Next Dialogue Tag", script.nextDialogueTag);
			script.dialogueText = EditorGUILayout.TextField("Dialogue Tag", script.dialogueTag);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("voice"), true);
		}
		else if (script.dialogueEntryType == DialogueEntryType.Fork)
		{
			EditorGUILayout.PropertyField(serializedObject.FindProperty("forkRequirements"), true);
			script.nextDialogueTag = EditorGUILayout.TextField("Dialogue Tag: Success", script.nextDialogueTag_Success);
			script.nextDialogueTag = EditorGUILayout.TextField("Dialogue Tag: Failure", script.nextDialogueTag_Failure);
		}
		else if (script.dialogueEntryType == DialogueEntryType.Choice)
		{
			EditorGUILayout.PropertyField(serializedObject.FindProperty("dialogueOptions"), true);
		}
	}
}
