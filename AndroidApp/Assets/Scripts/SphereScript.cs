using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour, IInteractable
{
    public void processDrag(Vector3 position, string dragType)
    {
        if(dragType == "Distance")
        {
            transform.position = position;
        }
    }

    public void select()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    public void deSelect()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }

    public void processScale(float scaleMultiplier)
    {
        Vector3 newScale = transform.localScale * scaleMultiplier;

        transform.localScale = newScale;
    }
}