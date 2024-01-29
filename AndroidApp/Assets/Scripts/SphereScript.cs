using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour, IInteractable
{
    private bool selected = false;
    public void processTap()
    {
        GetComponent<Renderer>().material.color = Color.yellow;
    }

    public void processDrag(Vector3 position)
    {
        throw new System.NotImplementedException();
    }
}