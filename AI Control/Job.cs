using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Job : MonoBehaviour
{
    public Vector3 position;
    public Machine target;
    public int requireMachineType;
    public int requireState; 
    public bool isTaken = false;
    public bool isCompleted = false;
    public JobRegister jobRegister;
    public Worker worker;
    public int jobType;
    public int id;
    private void Start(){
        jobRegister = JobRegister.instance;
    }
    public void TakeJob(Worker worker)
    {
        this.worker = worker;
        isTaken = true;
    }
    public void CompleteJob()
    {
        isTaken = false;
        isCompleted = true;
        worker = null;
    }
    public bool IsTaken()
    {
        return isTaken;
    }
    public Worker GetWorker()
    {
        return worker;
    }
    public Vector3 GetPosition()
    {
        return position;
    }
    
    public virtual void StartJob()
    {
        Debug.Log("Calling base class StartJob");
    }
}
public class PickJob : Job
{
    public PickJob()
    {
        jobType = 0;
        requireState = (int) (Machine.state.IDLE | Machine.state.PLACE | Machine.state.FINISHED);
    }
    public override void StartJob()
    {
        Debug.Log("worker: " +worker.id + " Starting PickJob id: " + id);
        worker.PickObject(target, position);  
    }
}
public class PlaceJob : Job
{
    public int productType;
    public PlaceJob(int productType=0)
    {
        jobType = 1;
        requireState = (int)Machine.state.IDLE;
        this.productType = productType;
    }
    public override void StartJob()
    {
        Debug.Log("worker:" + worker.id + " Starting PlaceJob id: " + id);
        worker.PlaceObject(target, position);
    }
}
public class OperateJob : Job
{
    public OperateJob()
    {
        jobType = 2;
        requireState = (int)Machine.state.PLACE;
    }
    public override void StartJob()
    {
        Debug.Log("Starting OperateJob id: " + id);
        worker.OperateObject(target, position);
    }
}
