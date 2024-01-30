using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CubeScript : MonoBehaviour, IInteractable
{
    public void processTap()
    {
        throw new System.NotImplementedException();
    }

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
