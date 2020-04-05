using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyComponent : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Jump), 8f, 8f);
    }

    // Update is called once per frame
    void Jump()
    {
        animator.SetTrigger("jump");
    }
}
