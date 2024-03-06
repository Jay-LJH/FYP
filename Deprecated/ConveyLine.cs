using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollConveyLine : MonoBehaviour,EventInterface
{
    public GameObject prefab;
    private double Create_speed = 1;
    bool state = false;
    Transform input;
    Vector3 input_pos;
    void Start()
    {
        StartCoroutine(CreatePrefabEverySecond());
        input = transform.Find("Input");
        input_pos = input.position;
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
        }
        if(result==5) //Thumb_up
        {
            state = true;
        }  
    }

    IEnumerator CreatePrefabEverySecond()
    {
        while (true)
        {
            if(state)
                Instantiate(prefab, input_pos, Quaternion.identity);
            yield return new WaitForSeconds((float)Create_speed); // 等待一秒
        }
    }
}
