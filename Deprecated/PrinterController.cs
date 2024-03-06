using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinterController : MonoBehaviour,EventInterface
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject prefab;
    private double Create_speed = 1;
    bool state = false;
    Light light;
    void Start()
    {
        light = transform.Find("Indicatorlight").gameObject.GetComponent<Light>();
        light.color = Color.red;
        StartCoroutine(CreatePrefabEverySecond());
    }
    public void RecognizeEvent(int result)
    {
        if(result==3) //Pointing_up
        {
            Create_speed*=0.9;
        }
        if(result==4) //Thumb_down
        {
            state = false;
            Create_speed = 1;
            light.color = Color.red;
        }
        if(result==5) //Thumb_up
        {
            state = true;
            light.color = Color.green;
        }  
    }

    IEnumerator CreatePrefabEverySecond()
    {
        while (true)
        {
            if(state)
                Instantiate(prefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds((float)Create_speed); // 等待一秒
        }
    }
}
