using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    public Transform InventoryPanel;

    public Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        //inventory = GetComponent<Inventory>();
        inventory.ItemAdded += InventoryItemAdd;
        inventory.ItemRemoved += InventoryItemRemove;
    }

    private void InventoryItemRemove(object sender, InventoryEventArgs e)
    {
        foreach (Transform slot in InventoryPanel)
        {
            Transform imageTransform = slot.GetChild(0).GetChild(0);
            Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();

            if (itemDragHandler == null) continue;
            if (itemDragHandler.Item == null) continue;

            if (itemDragHandler.Item.Equals(e.Item))
            {
                image.enabled = false;
                image.sprite = null;

                itemDragHandler.Item = null;
                break;
            }
        }
    }

    private void InventoryItemAdd(object sender, InventoryEventArgs e)
    {
        foreach (Transform slot in InventoryPanel)
        {
            Transform imageTransform = slot.GetChild(0).GetChild(0);
            Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();

            if (!image.enabled)
            {
                image.enabled = (true);
                image.sprite = e.Item.Image;

                itemDragHandler.Item = (BaseInventoryObject)e.Item;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
