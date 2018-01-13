using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueListItem : MonoBehaviour {

    public Text dialogueText;

    private CanvasScript canvasScript;
    private DialogueOption option;
    private DialogueChoice dialogueChoice;
    private Button thisButton;

    public void Populate(CanvasScript cs, DialogueOption option, DialogueChoice dialogueChoice, string dialogueText)
    {
        canvasScript = cs;

        this.option = option;
        this.dialogueChoice = dialogueChoice;
        this.dialogueText.text = dialogueText;

        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(ItemClick);

        thisButton.interactable = option.Check(dialogueChoice.GetNPC(), dialogueChoice.GetPlayer());
        if (!thisButton.IsInteractable())
        {
            this.dialogueText.color = Color.gray;
        }
    }
    
    private void ItemClick()
    {
        canvasScript.ChooseOption(option);
    }

    public void Over()
    {
        if (thisButton.IsInteractable())
        {
            this.dialogueText.color = Color.yellow;
        }
    }

    public void Out()
    {
        if (thisButton.IsInteractable())
        {
            this.dialogueText.color = Color.white;
        }
    }

}
