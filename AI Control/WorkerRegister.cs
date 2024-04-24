using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerRegister : MonoBehaviour
{
    [SerializeField]
    public int maxWorkers = 4;
    public GameObject workerPrefab;
    public static WorkerRegister instance;
    public List<Worker> workers = new List<Worker>();
    public List<Worker> availableWorkers = new List<Worker>();
    private void Awake()
    {
        instance = this;
        for (int i = 0; i < maxWorkers; i++)
        {
            GameObject worker = Instantiate(workerPrefab, new Vector3(2 * i, 0, 18), Quaternion.identity);
            Worker w = worker.GetComponent<Worker>();
            w.id = i;
            workers.Add(w);
            availableWorkers.Add(w);
        }
    }

    public void RegisterWorker(Worker worker)
    {
        workers.Add(worker);
    }

    public void UnregisterWorker(Worker worker)
    {
        workers.Remove(worker);
    }

    public Worker GetClosestAvailableWorker(Job job)
    {
        Worker closest = null;
        float minDistance = Mathf.Infinity;
        foreach (Worker w in availableWorkers)
        {
            if (w.isAvailableJob(job))
            {
                float distance = Vector3.Distance(w.transform.position, job.position);
                if (distance < minDistance)
                {
                    closest = w;
                    minDistance = distance;
                }
            }
        }
        return closest;
    }

    public void AddAvailableWorker(Worker worker)
    {
        availableWorkers.Add(worker);
    }
    public void RemoveAvailableWorker(Worker worker)
    {
        availableWorkers.Remove(worker);
    }
}
