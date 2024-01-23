using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour, IInteractable
{
    public void deselectObject(GameObject selectedObject)
    {
        GetComponent<Renderer>().material.color = Color.white;
    }

    public void processDrag(Vector3 newPos)
    {
        throw new System.NotImplementedException();
    }

    public void processTap()
    {
        //GetComponent<Renderer>().material.color = Color.yellow;
    }

    public void selectObject(GameObject selectedObject)
    {
        GetComponent<Renderer>().material.color = Color.yellow;
    }
}