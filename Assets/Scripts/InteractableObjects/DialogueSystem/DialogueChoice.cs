﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChoice : DialogueEntry {

    [Header("Dialogue Choice")]
    public List<DialogueOption> dialogueOptions;

    private void Start()
    {
        dialogueEntryType = DialogueEntryType.Choice;
    }
}
