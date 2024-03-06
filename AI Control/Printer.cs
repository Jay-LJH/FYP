using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printer: Machine
{
    public Printer()
    {
        type = 1;
        placePosition = new Vector3(0.25f, 1, 0);
    }
    public override void Start()
    {
        base.Start();
    }
    public override void pick(Worker worker)
    {
        Debug.Log("Starting pick at printer: " + id);
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
        Debug.Log("Starting place at printer: " + id);
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
        Debug.Log("Starting operate at printer: " + id);
        if(currentState == state.PLACE)
        {
            StartCoroutine(Print(5));
            currentState = state.FINISHED;
        }
        else{
            StartCoroutine(Print(10));
            currentState = state.FINISHED;
        }
    }
    IEnumerator Print(int time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Print finished");
    }
}