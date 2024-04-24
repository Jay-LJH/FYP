using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float mouseSensitivity = 2.0f;
    private float verticalRotation = 0;
    public float upDownRange = 60.0f;

    void Update()
    {
        // Character rotation
        float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, rotLeftRight, 0);

        // Camera rotation
        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        // Character movement
        float forwardSpeed = Input.GetAxis("Vertical") * speed;
        float sideSpeed = Input.GetAxis("Horizontal") * speed;
        Vector3 speed1 = new Vector3(sideSpeed, 0, forwardSpeed);
        speed1 = transform.rotation * speed1;
        CharacterController cc = GetComponent<CharacterController>();
        if(speed1.magnitude > 0.1f){
            GetComponent<Animator>().SetBool("IsWalk", true);  
        }
        else{
            GetComponent<Animator>().SetBool("IsWalk", false);
        }
        cc.SimpleMove(speed1);
    }
}