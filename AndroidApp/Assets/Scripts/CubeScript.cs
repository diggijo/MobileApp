using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour, IInteractable
{
    public void processDrag()
    {
        throw new System.NotImplementedException();
    }

    public void processTap()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }
}
