using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {

        [SerializeField] private ItemController _itemManager = new ItemController();

        [SerializeField] private LevelController _levelController = new LevelController();


        public Animator anim;
        void Start()
        {
            anim = GetComponent<Animator>();
            _levelController.Start();
            _itemManager.Start(this);
        }
        void Update()
        {
            _itemManager.Update();
        }
    }
}
