using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour, IInteractable
{
    public void processTap()
    {
        GetComponent<Renderer>().material.color = Color.yellow;
    }

    public void processDrag(Vector3 newPos)
    {
        throw new System.NotImplementedException();
    }
}