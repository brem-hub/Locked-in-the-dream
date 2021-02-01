using UnityEngine;

public interface IInventoryItem
{
    string Name { get; }
    Sprite Image { get; }

    bool IsPlaced { get; set; }
    void OnPickup();

    void OnDrop();
}