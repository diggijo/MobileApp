using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

public class GestureAction : MonoBehaviour
{
    private IInteractable selectedObject;
    private float hitDistance;

    internal void tapAt(Vector2 position)
    {
        DeselectObject();

        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo))
        {
            IInteractable objectHit = hitInfo.collider.gameObject.GetComponent<IInteractable>();
            hitDistance = Vector3.Distance(hitInfo.transform.position, Camera.main.transform.position);

            SelectObject(objectHit);
        }
    }

    internal void drag(Vector2 position)
    {
        if (selectedObject != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 newPos;

                if(hit.collider.CompareTag("DragPlane"))
                {
                    newPos = new Vector3(position.x, position.y, hit.point.z);
                }

                else if(hit.collider.CompareTag("Ground"))
                {
                    float liftHeight = .5f;
                    newPos = new Vector3(hit.point.x, hit.point.y + liftHeight, hit.point.z);
                }

                else
                {
                    return;
                }

                selectedObject.processDrag(newPos);
            }
        }
    }

    private void SelectObject(IInteractable newObject)
    {
        DeselectObject();

        selectedObject = newObject;
        selectedObject.select();
    }

    private void DeselectObject()
    {
        if (selectedObject != null)
        {
            selectedObject.deSelect();
            selectedObject = null;
        }
    }
}
