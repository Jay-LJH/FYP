using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    Worker w;
    void Start()
    {
        w = GetComponent<Worker>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.GetComponent<Machine>() != null && Vector3.Distance(transform.position, hit.collider.transform.position) < 2.5f)
                {
                    Machine m = hit.collider.GetComponent<Machine>();
                    Debug.Log("Machine type: " + m.type + "Machine ID" + m.id + "Machine state: " + m.currentState);
                    if (m.currentState == Machine.state.PLACE || m.currentState == Machine.state.FINISHED)
                    {
                        m.pick(w);
                    }
                    else if (m.type == 0) // always pick at storehouse
                    {
                        m.pick(w);
                    }
                    else if (m.currentState == Machine.state.IDLE && w.holdingObject != null)
                    {
                        m.place(w.holdingObject);
                    }
                }
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.GetComponent<Machine>() != null&& Vector3.Distance(transform.position, hit.collider.transform.position) < 2.5f)
                {
                    Machine m = hit.collider.GetComponent<Machine>();
                    if (m.currentState == Machine.state.PLACE)
                    {
                        m.operate();
                    }
                }
            }
        }
    }
    
}
