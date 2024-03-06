using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line: Machine{
    LinearConveyor linearConveyor;
    public override void Start()
    {
        base.Start();
        type = 3;
        linearConveyor = GetComponent<LinearConveyor>();
    }
    public override void operate()
    {
        if(linearConveyor.speed != 0)
            linearConveyor.ChangeSpeed(0);
        else
            linearConveyor.ChangeSpeed(1);
    }
}