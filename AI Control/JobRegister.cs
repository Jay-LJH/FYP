using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobRegister : MonoBehaviour
{
    public static JobRegister instance;
    public List<Job> jobs = new List<Job>();
    private WorkerRegister workerRegister;
    private MachineRegister machineRegister;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        workerRegister = WorkerRegister.instance;
        machineRegister = MachineRegister.instance;
    }
    public void RegisterJob(Job job)
    {
        jobs.Add(job);
    }

    public void UnregisterJob(Job job)
    {
        jobs.Remove(job);
    }
    
    public void FixedUpdate()
    {
        foreach (Job job in jobs)
        {
            if (!job.IsTaken() && !job.isCompleted)
            {
                Worker worker = workerRegister.GetAvailableWorker(job);
                Machine m = machineRegister.GetAvailableMachine(job.requireMachineType, job.requireState); 
                if (worker != null && m != null)
                {
                    job.position = m.operatePosition;
                    job.target = m;
                    job.TakeJob(worker);
                    worker.TakeJob(job);
                    break;
                }
            }
        }
    }
}
