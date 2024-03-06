using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveTo : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    public GameObject xField;
    public GameObject yField;
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    public void MoveToPosition()
    {
        TMPro.TMP_InputField XText = xField.GetComponent<TMPro.TMP_InputField>();
        TMPro.TMP_InputField YText = yField.GetComponent<TMPro.TMP_InputField>();
        float x = float.Parse(XText.text);
        float y = float.Parse(YText.text);
        agent.SetDestination(new Vector3(x, 0, y));
    }
}
