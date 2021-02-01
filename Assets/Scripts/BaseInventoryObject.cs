using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseInventoryObject : MonoBehaviour, IInventoryItem
{

    [SerializeField] private string _name;
    [SerializeField] private Sprite _image;

    [SerializeField] private PlaceSpot[] _placeSpots;

    private Camera _mainCamera;
    private bool _isPlaced = false;
    private PlaceSpot _chosenSpot;

    public string Name { get => _name; }
    public Sprite Image { get => _image; }

    public bool IsPlaced
    {
        get => _isPlaced;
        set => _isPlaced = value;
    }

    public Vector3 PlacePosition => _chosenSpot.transform.position;

    void Start()
    {
        _mainCamera = Camera.main;
    }

    public void SetPlacesActive(bool state)
    {
        foreach (var spot in _placeSpots)
        {
            spot.gameObject.SetActive(state);
        }
    }

    public bool CanBePlaced()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        var hits = Physics2D.GetRayIntersectionAll(ray);

        // Check if no other items are placed at this position
        if (hits.Length <= 0) return false;

        var hit = hits[0];
        foreach (var spot in _placeSpots)
        {
            if (hit.transform == spot.transform)
                if (spot.IsItemPlaced)
                    return false;
        }
        // If hit nothing - return
        if (hits.Length <= 0 || hits[0].collider == null) return false;

        foreach (var spot in _placeSpots)
        {
            if (spot.transform == hits[0].transform)
            {
                _chosenSpot = spot;
                _chosenSpot.Item = this;
                return true;
            }

        }
        return false;

    }
    public void OnPickup()
    {
        gameObject.SetActive(false);
    }

    public void OnDrop()
    {
        transform.position = new Vector3(PlacePosition.x, PlacePosition.y, 0);
        gameObject.SetActive(true);
        _chosenSpot.IsItemPlaced = true;
    }
}
