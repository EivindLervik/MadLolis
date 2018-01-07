using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueText : DialogueEntry {

    [Header("Dialogue Text")]
    public string nextDialogueTag;
    public string dialogueText;
    public AudioClip voice;

    private void Start()
    {
        dialogueEntryType = DialogueEntryType.Text;
    }
}
