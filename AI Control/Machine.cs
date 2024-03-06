using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Machine : MonoBehaviour
{
    public enum state
    {
        IDLE=1,
        BUSY=2,
        PLACE=0x4,
        FINISHED=0x8
    }
    public Vector3 operatePosition;
    public int id;
    public state currentState;
    public int type;  
    public static Vector3 placePosition;
    public GameObject ProductPrefab;
    public GameObject model;
    public static int idCount = 0;
    public virtual void Start()
    {
        currentState = state.IDLE;
    }
    public int GetState()
    {
        return (int)currentState;
    }
    public virtual void pick(Worker worker)
    {
        Debug.Log("Calling base class pick");
    }
    public virtual void place(GameObject target)
    {
        Debug.Log("Calling base class place");
    }
    public virtual void operate()
    {
        Debug.Log("Calling base class operate");
    }
}

