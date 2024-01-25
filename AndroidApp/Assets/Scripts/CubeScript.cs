using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CubeScript : MonoBehaviour, IInteractable
{
    public void processTap()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    public void processDrag(Vector3 position)
    {
        transform.position = position;
    }
}
