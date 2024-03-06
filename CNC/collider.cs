using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collider : MonoBehaviour
{
    void Start()
    {
     Debug.Log("Start");   
    }
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("OnTriggerEnter"+collider.transform.name);
        if(collider.transform.tag == "grabable" && CNCController.Instance.state == CNCController.State.Idle)
        {
            Transform parent = gameObject.transform.parent;
            Transform objectcollider = collider.gameObject.transform;
            objectcollider.SetParent(parent);
            objectcollider.localPosition = new Vector3(-0.89f,0.44f,0.02f);
            collider.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            collider.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            collider.gameObject.GetComponent<Rigidbody>().useGravity = false;
            Vector3 eulerAngle = new Vector3(90, -90, 0);
            Quaternion rotation = Quaternion.Euler(eulerAngle);
            objectcollider.rotation = rotation;
            CNCController.Instance.input = collider.gameObject;
            CNCController.Instance.processTime = System.Math.Sqrt(collider.gameObject.GetComponent<Rigidbody>().mass) * 10;
            CNCController.Instance.changeState(CNCController.State.Input);
        }   
    }
}
