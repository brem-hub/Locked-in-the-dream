using System;
using UnityEngine;

public class PlaceSpot : MonoBehaviour
{
    public static event Action<PlaceSpot> ItemPlaced;

    private bool _isItemPlaced;
    public BaseInventoryObject Item { get; set; }

    public bool IsItemPlaced
    {
        get => _isItemPlaced;
        set
        {
            _isItemPlaced = value;
            if (value)
                ItemPlaced?.Invoke(this);
        }

    }

    void Start()
    {
        IsItemPlaced = false;
    }

    void Update()
    {

    }
}
