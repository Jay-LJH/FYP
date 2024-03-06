using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperManager : MonoBehaviour
{
    private int time = 5;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestoryAfterTime());
    }
    IEnumerator DestoryAfterTime(){
        yield return new WaitForSeconds((float)time);
        Destroy(gameObject);
    }
}
