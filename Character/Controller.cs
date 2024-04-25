using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Controller : MonoBehaviour
{
    public bool MotionCapture = false;
    public bool isThirdPerson = true;
    public Transform firstPersonCamera;
    public Transform thirdPersonCamera;
    public GameObject UI;
    public Animator animator; // The Animator component
    public RuntimeAnimatorController firstPersonController; // The first person Animator Controller
    public RuntimeAnimatorController thirdPersonController; // The third person Animator Controller
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            MotionCapture = !MotionCapture;
            if(MotionCapture)
            {
                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<Animator>().enabled = false;
            }
            else
            {
                GetComponent<PlayerMovement>().enabled = true;
                GetComponent<Animator>().enabled = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            isThirdPerson = !isThirdPerson;
            SwitchCamera();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            animator.SetTrigger("play");
            UI.GetComponent<TMP_Text>().text = "Avatar replaying the animation capture data";
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            UI.GetComponent<TMP_Text>().text = "Recording";
        }
        else if(Input.GetKeyUp(KeyCode.X))
        {
            UI.GetComponent<TMP_Text>().text = "Not Recording";
        }
    }
    void SwitchCamera()
    {
        if (isThirdPerson)
        {
            thirdPersonCamera.gameObject.SetActive(true);
            firstPersonCamera.gameObject.SetActive(false);
            animator.runtimeAnimatorController = thirdPersonController;
            animator.applyRootMotion = true;
            UI.GetComponent<TMP_Text>().text = "Operator offline";
        }
        else
        {
            thirdPersonCamera.gameObject.SetActive(false);
            firstPersonCamera.gameObject.SetActive(true);
            animator.runtimeAnimatorController = firstPersonController;
            animator.applyRootMotion = false;
            UI.GetComponent<TMP_Text>().text = "Not Recording";
        }
    }
}
