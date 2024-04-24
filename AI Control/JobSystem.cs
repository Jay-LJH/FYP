using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobSystem : MonoBehaviour
{
    public static JobSystem instance;
    public static int jobCount = 1000;
    public JobRegister jobRegister;
    public WorkerRegister workerRegister;
    public MachineRegister machineRegister;
    public instrutionrecorder recorder;

    private void Start()
    {
        instance = this;
        jobRegister = JobRegister.instance;
        workerRegister = WorkerRegister.instance;
        machineRegister = MachineRegister.instance;
        recorder = instrutionrecorder.instance;
    }
    //this function create a series of test job
    public void createPickJob(int machineType = 0)
    {
        Job job = new PickJob();
        job.requireMachineType = machineType;
        job.id = jobCount++;
        jobRegister.RegisterJob(job);
        recorder.AddInstrution(job);
    }
    public void createPlaceJob(int machineType = 1)
    {
        Job job = new PlaceJob();
        job.requireMachineType = machineType;
        job.id = jobCount++;
        jobRegister.RegisterJob(job);
        recorder.AddInstrution(job);
    }
    public void createOperateJob(int machineType = 2)
    {
        Job job = new OperateJob();
        job.requireMachineType = machineType;
        job.id = jobCount++;
        jobRegister.RegisterJob(job);
        recorder.AddInstrution(job);
    }
}
