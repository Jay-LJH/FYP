using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 20.0f;
    private float bodyMass;
    private Rigidbody rb;
    private bool isJumping = false;
    [SerializeField]
    private float jumpHeigh = 1.0f;
    private Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator =transform.Find("Character").gameObject.GetComponent<Animator>();
        bodyMass = rb.mass;
    }
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            animator.SetTrigger("Jump");
        }
    }
    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 desiredMoveDirection = forward * moveVertical + right * moveHorizontal;
        Vector3 movement = desiredMoveDirection * speed * bodyMass;

        rb.AddForce(movement);
        if (isJumping)
        {
            rb.AddForce(new Vector3(0, jumpHeigh * bodyMass, 0), ForceMode.Impulse);
            isJumping = false;
        }
        animator.SetBool("IsWalking", rb.velocity.magnitude > 0);
    }
}