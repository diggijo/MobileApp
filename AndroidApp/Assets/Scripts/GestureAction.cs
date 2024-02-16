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

    internal void tapAt(Touch t)
    {
        Vector2 position = t.position;

        if(selectedObject != null)
        {
            DeselectObject();
        }

        Ray ray = Camera.main.ScreenPointToRay(position);

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

    internal void drag(Touch t)
    {
        Vector2 position = t.position;
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
            Vector2 delta = t.deltaPosition;

            float cameraSpeed = 0.01f;
            Vector3 cameraMovement = new Vector3(-delta.x, -delta.y, 0) * cameraSpeed;
            Camera.main.transform.Translate(cameraMovement, Space.World);
        }
    }

    internal void pinch(float pinchDelta)
    {
        float pinchScaleFactor = 0.01f;

        if (selectedObject != null)
        {
            float scaleMultiplier = 1.0f + pinchDelta * pinchScaleFactor;

            selectedObject.processScale(scaleMultiplier);
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
