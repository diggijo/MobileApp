using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleScript : MonoBehaviour, IInteractable
{
    public void ProcessDrag(Vector3 position, string dragType)
    {
        if(dragType == "DragPlane")
        {
            transform.position = Camera.main.ScreenToWorldPoint(position);
        }
    }

    public void Select()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    public void DeSelect()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }

    public void ProcessScale(float scaleMultiplier)
    {
        Vector3 newScale = transform.localScale * scaleMultiplier;

        transform.localScale = newScale;
    }

    public void ProcessRotation(float rotation)
    {
        throw new System.NotImplementedException();
    }
}
