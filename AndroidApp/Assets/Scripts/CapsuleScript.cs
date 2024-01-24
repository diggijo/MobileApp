using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleScript : MonoBehaviour, IInteractable
{
    public void processTap()
    {
        GetComponent<Renderer>().material.color = Color.blue;
    }

    public void processDrag(Vector3 newPos)
    {
        throw new System.NotImplementedException();
    }
}
