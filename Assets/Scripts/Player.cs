using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {

        [SerializeField] private ItemController _itemManager = new ItemController();

        [NonSerialized] public Animator anim;
        void Start()
        {
            anim = GetComponent<Animator>();
            _itemManager.Start(this);
        }
        void Update()
        {
            _itemManager.Update();
        }
    }
}
