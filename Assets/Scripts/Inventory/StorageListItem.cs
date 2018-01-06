using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum StorageListItemClickAction
{
    Move, Drop, Use
}

public class StorageListItem : MonoBehaviour {

    public Text description;
    public Text itemName;
    public Text amount;
    public Text weight;
    public Image image;

    private CanvasScript canvasScript;
    private RectTransform myMaster;
    private StorageItem storageItem;
    private Button thisButton;

	public void Populate(CanvasScript cs, RectTransform myMaster, StorageItem si, int amount, StorageListItemClickAction action)
    {
        canvasScript = cs;
        this.myMaster = myMaster;
        this.storageItem = si;

        this.description.text = si.description;
        this.itemName.text = si.itemName;
        this.amount.text = amount.ToString();
        this.weight.text = (si.weight * amount).ToString() + " kg";
        this.image.sprite = si.icon;

        thisButton = GetComponent<Button>();
        switch (action)
        {
            case StorageListItemClickAction.Drop:
                thisButton.onClick.AddListener(ItemDrop);
                break;
            case StorageListItemClickAction.Move:
                thisButton.onClick.AddListener(ItemMove);
                break;
            case StorageListItemClickAction.Use:
                thisButton.onClick.AddListener(ItemUse);
                break;
        }
    }

    public void UpdateAmount(int amount, StorageItem si)
    {
        this.amount.text = amount.ToString();
        this.weight.text = (si.weight * amount).ToString() + " kg";
    }

    private void ItemDrop()
    {
        // Drop an item
    }

    private void ItemMove()
    {
        canvasScript.MoveItemToOtherThan(myMaster, this);
    }

    private void ItemUse()
    {
        // Use an item
    }

    public RectTransform GetMyMaster()
    {
        return myMaster;
    }

    public StorageItem GetStorageItem()
    {
        return storageItem;
    }

}
