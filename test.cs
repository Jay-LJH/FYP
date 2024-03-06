using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField]
    public GameObject ProductPrefab;
    private void Start(){
        GameObject product = GameObject.Instantiate(ProductPrefab);
        bool isSame = product == ProductPrefab;
        Debug.Log("isSame: " + isSame);
    }

}
