using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    class ItemController
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Transform _inventoryHUD;

        private Camera _mainCamera;
        
        private bool _moving;
        private Vector3 _targetPoint;
        private IInventoryItem _itemToGather;
        private BaseInventoryObject _itemToPlace;
        private Player _owner;
        private bool _movingRight;
        private const float ACTIVE_DISTANCE = 1f;

        public void Start(Player owner)
        {
            _movingRight = true;
            _owner = owner;
            _moving = false;
            _owner.anim.SetBool("movement", false);
            _owner.anim.Play("Idle");

            _mainCamera = Camera.main;
            ItemDropHandler.ItemPlaced += SetItemForPlacing;
        }

        public void Update()
        {
            if (_moving)
                MoveTowardsPoint();

            else if (Input.GetMouseButtonDown(0))
            {
                var pos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

                SetItemForGathering();

                RectTransform inventPanel = _inventoryHUD as RectTransform;
                if (!RectTransformUtility.RectangleContainsScreenPoint(inventPanel, Input.mousePosition))
                    StartMoving(pos);
            }
        }
        private IInventoryItem GetItemAtPos(Vector3 pos)
        {
            Ray ray = _mainCamera.ScreenPointToRay(pos);


            RaycastHit2D hits = Physics2D.GetRayIntersection(ray);

            if (hits.collider == null) return null;

            IInventoryItem item = hits.collider.gameObject.GetComponent<IInventoryItem>();
            return item;

        }

        private void StartMoving(Vector2 pos)
        {
            _targetPoint = pos;
            _moving = true;
            _owner.anim.SetBool("movement", true);
            _owner.anim.Play("Movement");


            //MoveTowardsPoint();
        }
        private void GatherItem(IInventoryItem item)
        {
            _inventory.AddItem(item);
            _itemToGather = null;
        }

        private void PlaceItem()
        {
            _itemToPlace.SetPlacesActive(false);
            
            _inventory.RemoveItem(_itemToPlace);
            
            _itemToPlace.IsPlaced = true;
            _itemToPlace.OnDrop();
            _itemToPlace = null;
        }

        private void MoveTowardsPoint()
        {
            if (_targetPoint.x < _owner.transform.position.x)
            {
                if (_movingRight)
                {
                    Vector3 theScale = _owner.transform.localScale;
                    theScale.x *= -1;
                    _owner.transform.localScale = theScale;
                    _movingRight = !_movingRight;
                }
            }
            else
            {
                if (!_movingRight)
                {
                    Vector3 theScale = _owner.transform.localScale;
                    theScale.x *= -1;
                    _owner.transform.localScale = theScale;
                    _movingRight = !_movingRight;
                }
            }

            if (Mathf.Abs(_owner.transform.position.x - _targetPoint.x) < ACTIVE_DISTANCE)
            {
                _moving = false;
                _owner.anim.SetBool("movement", false);
                _owner.anim.Play("Idle");
                _targetPoint = Vector3.zero;

                if (_itemToGather != null) GatherItem(_itemToGather);

                if (_itemToPlace != null) PlaceItem();
            }
            else
                _owner.transform.position = Vector2.MoveTowards(_owner.transform.position, new Vector2(_targetPoint.x, _owner.transform.position.y), 0.1f);
        }

        private void SetItemForGathering()
        {
            var item = GetItemAtPos(Input.mousePosition);

            if (item == null) return;

            if (item.IsPlaced) return;

            if (Mathf.Abs(_owner.transform.position.x - _targetPoint.x) < ACTIVE_DISTANCE)
                GatherItem(item);
            else
                _itemToGather = item;
        }

        public void SetItemForPlacing(BaseInventoryObject item)
        {
            _itemToPlace = item;

            if (Mathf.Abs(_owner.transform.position.x - item.PlacePosition.x) < ACTIVE_DISTANCE)
                PlaceItem();
            else
            {
                _targetPoint = _itemToPlace.PlacePosition;
                _owner.anim.SetBool("movement", true);
                _owner.anim.Play("Movement");
                _moving = true;
            }
        }
    }
}
