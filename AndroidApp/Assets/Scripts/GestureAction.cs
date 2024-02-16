using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class GestureAction : MonoBehaviour
{
    private IInteractable selectedObject;
    private float hitDistance;

    internal void tapAt(Vector2 position)
    {
        if(selectedObject != null)
        {
            DeselectObject();
        }

        Ray ray = Camera.main.ScreenPointToRay(position);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 10);

        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo))
        {
            IInteractable objectHit = hitInfo.collider.gameObject.GetComponent<IInteractable>();

            if (objectHit != null)
            {
                hitDistance = Vector3.Distance(hitInfo.transform.position, Camera.main.transform.position);
                SelectObject(objectHit);
            }
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
                string dragType = "";

                if(hit.collider.CompareTag("DragPlane"))
                {
                    dragType = "DragPlane";
                    newPos = new Vector3(position.x, position.y, hit.point.z);
                }

                else if(hit.collider.CompareTag("Ground"))
                {
                    dragType = "Ground";
                    newPos = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                }

                else
                {
                    dragType = "Distance";
                    newPos = ray.GetPoint(hitDistance);
                }

                selectedObject.processDrag(newPos, dragType);
            }
        }
        else
        {
            // Code to move the camera when selectedObject is null
            // Example: Move the camera based on input position
            Camera.main.transform.Translate(new Vector3(position.x, position.y, 0) * Time.deltaTime);
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
