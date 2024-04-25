using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instrutionrecorder : MonoBehaviour
{
    public bool isRecording;
    public List<Job> instrutions;
    public static instrutionrecorder instance;
    public JobRegister jobRegister;
    public void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        jobRegister = JobRegister.instance;
    }
    public void StartRecording()
    {
        isRecording = true;
        instrutions = new List<Job>();
    }
    public void StopRecording()
    {
        isRecording = false;
    }
    public void AddInstrution(Job j)
    {
        if (isRecording)
        {
            instrutions.Add(j);
        }
    }
    public void replay()
    {
        foreach (var j in instrutions)
        {
            var job = j.Clone();
            job.id = JobSystem.jobCount++;
            jobRegister.RegisterJob(job);
        }
    }
}
