using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InventoryEventArgs : EventArgs
{
    public IInventoryItem Item;
    public InventoryEventArgs(IInventoryItem item)
    {
        Item = item;
    }
}

public class Inventory : MonoBehaviour
{

    private const int SLOTS = 9;

    private List<IInventoryItem> _items = new List<IInventoryItem>();

    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> ItemRemoved;

    public void AddItem(IInventoryItem item)
    {
        if (_items.Count < SLOTS)
        {
            Collider2D col = (item as MonoBehaviour)?.GetComponent<Collider2D>();

            if (col != null && col.enabled)
            {
                col.enabled = false;
                _items.Add(item);
                item.OnPickup();
                ItemAdded?.Invoke(this, new InventoryEventArgs(item));
            }
        }
    }

    public void RemoveItem(IInventoryItem item)
    {
        if (_items.Contains(item))
        {
            _items.Remove(item);

            Collider2D col = (item as MonoBehaviour).GetComponent<Collider2D>();
            if (col != null)
            {
                col.enabled = true;
            }

            ItemRemoved?.Invoke(this, new InventoryEventArgs(item));
        }
    }
}
