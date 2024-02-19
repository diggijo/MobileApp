using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CubeScript : MonoBehaviour, IInteractable
{
    float liftHeight;
    public void ProcessDrag(Vector3 position, string dragType)
    {
        if (dragType == "Ground")
        {
            liftHeight = transform.localScale.y * 0.5f;
            transform.position = new Vector3(position.x, position.y + liftHeight, position.z);
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
        transform.Rotate(Vector3.forward, rotation);
    }
}
