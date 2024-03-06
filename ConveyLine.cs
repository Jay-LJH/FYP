using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;

public class ConveyLine : MonoBehaviour
{
    public Animator anim;
    public bool isStart;
    public float startSpeed = 1f;
    public float stopSpeed = 0f;
    public LinearConveyor line;
    Tween tween;

    private void Start()
    {
        StopLine();
    }
    private void OnMouseDown()
    {
        if (isStart)
        {
            anim.SetTrigger("Press");
            tween.Stop();
            tween = Tween.Delay(1f, () => StopLine());
        }
        else
        {
            anim.SetTrigger("Press");
            tween.Stop();
            tween = Tween.Delay(1f, () => StartLine());   
        }
    }

    public void StartLine()
    {
        isStart = true;
        line.ChangeSpeed(startSpeed);
    }
    public void StopLine()
    {
        isStart = false;
        line.ChangeSpeed(stopSpeed);
    }
}

