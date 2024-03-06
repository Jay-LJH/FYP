using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public interface EventInterface : IEventSystemHandler
{
    void RecognizeEvent(int result);
}