using System;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{
    private Player player;
    
    public Inventory Inventory;

    public static event Action<BaseInventoryObject> ItemPlaced;
    
    void Start()
    {
        player = FindObjectOfType<Player>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        RectTransform inventPanel = transform as RectTransform;

        if (!RectTransformUtility.RectangleContainsScreenPoint(inventPanel, Input.mousePosition))
        {
            BaseInventoryObject item = eventData.pointerDrag.gameObject.GetComponent<ItemDragHandler>().Item;
            if (item == null) return;
            if (!item.CanBePlaced()) return;
            
            ItemPlaced?.Invoke(item);
        }
    }
}
