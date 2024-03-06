using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInteraction : MonoBehaviour
{
    enum state
    {
        waiting,
        grabing,
        holding,
        throwing
    }
    private GameObject selectedObject;
    private float distance = 1.5f;
    private state holdingState = state.waiting;
    private float throwForce = 10.0f;
    private float grabdistance = 5.0f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && holdingState == state.holding)
        {
            holdingState = state.waiting;
            selectedObject.GetComponent<Rigidbody>().useGravity = true; 
            gameObject.GetComponent<Rigidbody>().mass -= selectedObject.GetComponent<Rigidbody>().mass;
            selectedObject = null;
        }
        if (Input.GetMouseButtonDown(1) && holdingState == state.holding)
        {
            holdingState = state.waiting;
            selectedObject.GetComponent<Rigidbody>().useGravity = true; 
            selectedObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);
            gameObject.GetComponent<Rigidbody>().mass -= selectedObject.GetComponent<Rigidbody>().mass;
            selectedObject = null;
        }
        else if (Input.GetMouseButtonDown(0) && holdingState == state.waiting)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                selectedObject = hit.transform.gameObject;
                if(selectedObject.tag == "grabable" && Vector3.Distance(selectedObject.transform.position, gameObject.transform.position) < grabdistance)
                {
                    holdingState = state.grabing;
                    selectedObject.GetComponent<Rigidbody>().useGravity = false;
                    StartCoroutine("GrabObjectLerp",Time.time);
                }
            }
        }
        if(holdingState == state.holding)
        {
            selectedObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * distance;
        }
    }
    IEnumerator GrabObjectLerp(float startTime)
    {
        Debug.Log("GrabObjectLerp");
        gameObject.GetComponent<Rigidbody>().mass += selectedObject.GetComponent<Rigidbody>().mass;
        float grabtime = selectedObject.GetComponent<Rigidbody>().mass/10.0f;
        Vector3 start = selectedObject.transform.position;
        while (holdingState == state.grabing)
        {
            float t = (Time.time - startTime) / grabtime;
            selectedObject.transform.position = Vector3.Lerp(start, Camera.main.transform.position + Camera.main.transform.forward * distance, t);
            if (t >= 1)
            {
                holdingState = state.holding;
            }
            yield return null;
        }
        Debug.Log("GrabObjectLerp End");
    }
}
