using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public Animator animator;

    public float moveSpeed = 3f;

    public float rotateSmooth = 10f;

    public Transform model;

    public Transform cameraTF;
    
    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {

            moveDirection = cameraTF.TransformDirection(moveDirection);
            MovePlayer(moveDirection);  
            RotatePlayer(moveDirection);
            animator.SetBool(name: "IsWalk", value: true);
        }
        else
        {
            animator.SetBool(name: "IsWalk", value: false);
        }
    }
    void MovePlayer(Vector3 moveDirection)
    {
        
        moveDirection.y = 0f;
        
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }
    void RotatePlayer(Vector3 moveDirection)
    {
        
        Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
        
        toRotation = Quaternion.Euler(0f, toRotation.eulerAngles.y, 0f);
        model.rotation = Quaternion.Slerp(model.rotation, toRotation, Time.deltaTime * rotateSmooth);
    }

}
