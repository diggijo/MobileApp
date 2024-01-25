using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CubeScript : MonoBehaviour, IInteractable
{
    private bool selected = false;
    public void processTap()
    {
        //GetComponent<Renderer>().material.color = Color.red;
    }

    public void processDrag(Vector3 position)
    {
        transform.position = Camera.main.ScreenToWorldPoint(position);
    }
}
