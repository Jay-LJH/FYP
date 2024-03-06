using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialFactory : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;
    private void OntriggerExit(Collider other)
    {
        Debug.Log("OntriggerExit");
        GameObject output = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(90, -90, 0)));
    }
}
