using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_Dialogue : GUI_Element {

    [Header("Dialogue")]
    public Text dialogue_Header;
    public Text dialogue_Text;
    public RectTransform dialogue_Choices;
    public RectTransform dialogue_DialogueEntry;
    public RectTransform dialogue_DialogueInventory;
    private List<DialogueListItem> dialogue_DialogueListOfEntries;
    public float dialogue_ItemsPerPage;
    public float dialogue_ItemsSpace;
    public float dialogue_ItemsTopSpace;
    public float dialogue_ItemsBottomSpace;
    public GameObject dialogue_Item;
    public KeyCode continueKey;
    private DialogueTree dialogueTree;
    private DialogueEntry currentEntry;

    private CanvasScript cs;

    private void Update()
    {
        if (dialogueTree != null && currentEntry != null)
        {
            if (Input.GetKeyDown(continueKey))
            {
                ContinueDialogue();
            }
        }
    }

    public void OpenDialogueMenu(NPC npc, CanvasScript cs, Character player)
    {
        this.cs = cs;

        DialogueEntry de;
        dialogueTree = npc.StartDialogue(player, out de);
        currentEntry = de;

        dialogue_Header.text = npc.characterName;
        DisplayDialogueEntry();
    }
    private void ContinueDialogue()
    {
        currentEntry = dialogueTree.Proceed(currentEntry);
        DialogueIsEnd();
    }
    public void ChooseOption(DialogueOption option)
    {
        currentEntry = dialogueTree.ChooseCoice(option);
        DialogueIsEnd();
    }
    private void DialogueIsEnd()
    {
        if (currentEntry == null)
        {
            cs.CloseDialogueMenu();
        }
        else
        {
            DisplayDialogueEntry();
        }
    }
    private void DisplayDialogueEntry()
    {
        if (currentEntry.GetDialogueEntryType() == DialogueEntryType.Text)
        {
            dialogue_Text.gameObject.SetActive(true);
            dialogue_Choices.gameObject.SetActive(false);

            DialogueText dt = (DialogueText)currentEntry;
            dialogue_Text.text = dt.dialogueText;
        }
        else if (currentEntry.GetDialogueEntryType() == DialogueEntryType.Fork)
        {
            dialogue_Text.gameObject.SetActive(false);
            dialogue_Choices.gameObject.SetActive(false);

            ContinueDialogue();
        }
        else if (currentEntry.GetDialogueEntryType() == DialogueEntryType.Choice)
        {
            dialogue_Text.gameObject.SetActive(false);
            dialogue_Choices.gameObject.SetActive(true);

            float dialogue_frameHeight = dialogue_DialogueInventory.rect.height;
            float dialogue_itemHeight = dialogue_frameHeight / dialogue_ItemsPerPage;
            float dialogue_itemSpace = dialogue_itemHeight / dialogue_ItemsSpace;

            dialogue_DialogueListOfEntries = new List<DialogueListItem>();

            DialogueChoice dc = (DialogueChoice)currentEntry;
            dialogue_DialogueEntry.sizeDelta = new Vector2(dialogue_DialogueEntry.sizeDelta.x, (dc.dialogueOptions.Count * (dialogue_itemHeight + dialogue_itemSpace)) + dialogue_ItemsBottomSpace + dialogue_ItemsTopSpace);

            int index = 0;
            foreach (DialogueOption option in dc.dialogueOptions)
            {
                DialogueListItem dli = Instantiate(dialogue_Item, dialogue_DialogueEntry).GetComponent<DialogueListItem>();
                RectTransform itemRect = dli.GetComponent<RectTransform>();

                dialogue_DialogueListOfEntries.Add(dli);

                Vector2 itemRectSize = itemRect.sizeDelta;
                itemRectSize.y = dialogue_itemHeight;
                itemRect.sizeDelta = itemRectSize;

                Vector3 itemRectPos = itemRect.localPosition;
                itemRectPos.y -= dialogue_ItemsTopSpace + (index * (dialogue_itemSpace + dialogue_itemHeight));
                itemRect.localPosition = itemRectPos;

                dli.Populate(this, option, dc, option.optionText);

                index++;
            }
        }
    }
    public void CloseDialogueMenu()
    {
        currentEntry = null;
        dialogueTree = null;
        currentEntry = null;
    }
}
