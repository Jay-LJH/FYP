using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRay : MonoBehaviour,EventInterface
{
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.transform.name == "StartButton")
                {
                    CNCController.Instance.StartButton();
                }
                else if(hit.transform.name == "StopButton")
                {
                    CNCController.Instance.StopButton();
                }
                else if(hit.transform.name == "PauseButton")
                {
                    CNCController.Instance.PauseButton();
                }
            }
        }
    }
    public void RecognizeEvent(int result)
    {
        Debug.Log("RecognizeEvent");
        if(result == 5) //Thumb_up
        {
            CNCController.Instance.StartButton();
        }
        else if(result == 4) //Thumb_down
        {
            CNCController.Instance.StopButton();
        }
        else if(result == 2) //Open_Palm
        {
            CNCController.Instance.PauseButton();
        }
    }
}
