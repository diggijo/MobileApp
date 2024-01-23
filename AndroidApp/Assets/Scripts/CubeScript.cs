using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour, IInteractable
{
    public void deselectObject(GameObject selectedObject)
    {
        GetComponent<Renderer>().material.color = Color.white;
    }

    public void processDrag(Vector3 newPos)
    {
        transform.position = newPos;
    }

    public void processTap()
    {
        //GetComponent<Renderer>().material.color = Color.red;
    }

    public void selectObject(GameObject selectedObject)
    {
        GetComponent<Renderer>().material.color = Color.yellow;
    }
}
