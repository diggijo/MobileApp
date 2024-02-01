using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour, IInteractable
{
    public void processDrag(Vector3 position)
    {
        transform.position = Camera.main.ScreenToWorldPoint(position);
    }

    public void select()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    public void deSelect()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
}