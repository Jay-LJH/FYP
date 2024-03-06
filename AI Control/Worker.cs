using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Worker : MonoBehaviour
{
    private WorkerRegister workerRegister;
    public Job currentJob;
    UnityEngine.AI.NavMeshAgent agent;
    public GameObject holdingObject;
    public int id;
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        workerRegister = WorkerRegister.instance;
        holdingObject = null;
    }
    public bool isAvailableJob(Job job)
    {
        if(currentJob == null)
        {
            if(job.jobType == 0)  //pick
            {
                if(holdingObject == null)
                {
                    return true;
                }
            }
            else if(job.jobType == 1)  //place
            {
                
                if(holdingObject != null && holdingObject.GetComponent<Product>().type == ((PlaceJob)job).productType)
                {
                    return true;
                }
            }
            else if(job.jobType == 2) //operate
            {
                if(holdingObject == null)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public void TakeJob(Job job)
    {
        currentJob = job;
        workerRegister.RemoveAvailableWorker(this);
        job.StartJob();
    }
    public void CompleteJob()
    {
        currentJob.CompleteJob();
        Debug.Log("Job Completed: " + currentJob.id);
        currentJob = null;
        workerRegister.AddAvailableWorker(this);
    }
    private void OnDestroy()
    {
        workerRegister.UnregisterWorker(this);
    }
    public void PickObject(Machine target, Vector3 position)
    {
        StartCoroutine(PickObjectCoroutine(target, position));
    }
    public void PlaceObject(Machine target, Vector3 position)
    {
        StartCoroutine(PlaceObjectCoroutine(target, position));
    }
    public void OperateObject(Machine target, Vector3 position)
    {
        StartCoroutine(OperateObjectCoroutine(target, position));
    }
    public IEnumerator PickObjectCoroutine(Machine target, Vector3 position)
    {
        agent.SetDestination(position);
        while (Vector3.Distance(transform.position, position) > 1f)
        {
            yield return null; // Wait for the next frame
        }
        target.pick(this);
        CompleteJob();
    }
    public IEnumerator PlaceObjectCoroutine(Machine target, Vector3 position)
    {
        agent.SetDestination(position);
        while (Vector3.Distance(transform.position, position) > 1f)
        {
            yield return null; // Wait for the next frame
        }
        target.place(holdingObject);
        holdingObject = null;
        CompleteJob();
    }
    public IEnumerator OperateObjectCoroutine(Machine target, Vector3 position)
    {
        agent.SetDestination(position);
        while (Vector3.Distance(transform.position, position) > 1f)
        {
            yield return null; // Wait for the next frame
        }
        target.operate();
        CompleteJob();
    }
}
