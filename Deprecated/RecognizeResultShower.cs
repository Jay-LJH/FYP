using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecognizeResultShower : MonoBehaviour,EventInterface
{
    [SerializeField]
    Texture[] textures;

    public void RecognizeEvent(int result)
    {
        GetComponent<Renderer>().material.mainTexture = textures[result];
    }
}
