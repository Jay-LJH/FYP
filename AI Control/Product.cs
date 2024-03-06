using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    public int id;
    public int type;
    public enum state
    {
        Place,
        Pick,
        Free
    }
    public state currentState;
    public void ChangeState(state newState)
    {
        currentState = newState;
        if(newState == state.Free)
        {
            transform.SetParent(null);
            GetComponent<Rigidbody>().isKinematic = false;
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
