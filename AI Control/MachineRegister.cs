using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineRegister : MonoBehaviour
{
    [SerializeField]
    public int maxMachines = 4;
    public static MachineRegister instance;
    public List<Machine> machines = new List<Machine>();
    public List<Machine> availableMachines = new List<Machine>();
    public GameObject CNCPrefab;
    public GameObject PrinterPrefab;
    public GameObject StorehousePrefab;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.GetComponent<Machine>() != null)
            {
                RegisterMachine(obj.GetComponent<Machine>());
                availableMachines.Add(obj.GetComponent<Machine>());
            }
        }
    }
    public void RegisterMachine(Machine machine)
    {
        machines.Add(machine);
    }

    public void UnregisterMachine(Machine machine)
    {
        machines.Remove(machine);
    }

    public Machine GetAvailableMachine(int type, int state)
    {
        foreach (Machine m in availableMachines)
        {
            if (m.type == type && ((int)(m.currentState) & state)!=0)
            {
                return m;
            }
        }
        return null;
    }

    public void AddAvailableMachine(Machine machine)
    {
        availableMachines.Add(machine);
    }
    public void RemoveAvailableMachine(Machine machine)
    {
        availableMachines.Remove(machine);
    }
}
