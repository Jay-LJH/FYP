using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour,EventInterface
{
    private Animator animator;
    private Rigidbody rb;
    private Vector3 lastPosition;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator =gameObject.GetComponent<Animator>();
    } 
    void Update()
    {
        animator.SetBool(name: "IsWalk", value: lastPosition != transform.position);
        lastPosition = transform.position;
    }
    public void RecognizeEvent(int result)
    {
        if(result==2){
            animator.SetTrigger("Waving");
        }
    }
}
