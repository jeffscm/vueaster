using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyHopComponent : MonoBehaviour
{
    public Transform target, zeroPosition;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Hop), 5f, 5f);    
    }

    private void OnEnable()
    {
        this.transform.position = zeroPosition.position;
    }
    // Update is called once per frame
    void Hop()
    {
        var savedPosition = animator.transform.position;
        this.transform.position = target.position;
        animator.transform.position = savedPosition;

        var rnd = Random.Range(-20, 20);
        var euler = this.transform.localEulerAngles;
        euler.y += rnd;
        this.transform.localEulerAngles = euler;

        animator.SetTrigger("hop"); 
    }
}
