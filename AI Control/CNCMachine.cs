using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CNCMachine: Machine
{
    public CNCMachine()
    {
        type = 2;
        placePosition = new Vector3(0, 1, 0);
    }
    public override void Start()
    {
        base.Start();
    }
    public override void pick(Worker worker)
    {
        Debug.Log("Starting pick at CNCMachine: " + id);
        if(currentState == state.PLACE || currentState == state.FINISHED)
        {
            currentState = state.IDLE;
            worker.holdingObject = model;
            model.transform.SetParent(worker.transform);
            model.transform.localPosition = worker.transform.forward;
            model.GetComponent<Product>().ChangeState(Product.state.Pick);
        }
    }
    public override void place(GameObject target)
    {
        Debug.Log("Starting place at CNCMachine: " + id);
        if(currentState == state.IDLE)
        {
            currentState = state.PLACE;
            model = target;
            target.transform.SetParent(this.transform);
            target.transform.localPosition = placePosition;
            model.GetComponent<Product>().ChangeState(Product.state.Place);
        }
    }
    public override void operate()
    {
        Debug.Log("Starting operate at CNCMachine: " + id);
        if(currentState == state.PLACE)
        {
            StartCoroutine(Operate());
            currentState = state.FINISHED;
        }
        else{
            Debug.Log("CNCMachine is not ready, place first!");
        }
    }
    IEnumerator Operate()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("CNCMachine finished");
    }
}