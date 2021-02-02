using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimation : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.speed = 1f;
        anim.speed = anim.GetFloat("Animation Speed");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
