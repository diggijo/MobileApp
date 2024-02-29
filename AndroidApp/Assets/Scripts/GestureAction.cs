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
    private float rotationSpeed;

    internal void TapAt(Touch t)
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

    internal void Drag(Touch t)
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

                selectedObject.ProcessDrag(newPos, dragType);
            }
        }
        else
        {
            //NEW ROTATION METHOD TO BE BUILT IN



            // TO BE IMPLEMENTED IN A SWIPE
            /*Vector2 delta = t.deltaPosition;

            float cameraSpeed = 0.01f; 

            Vector3 cameraRight = Camera.main.transform.right;
            Vector3 cameraUp = Camera.main.transform.up;

            Vector3 cameraMovement = (cameraRight * -delta.x + cameraUp * -delta.y) * cameraSpeed;
            Camera.main.transform.Translate(cameraMovement, Space.World);*/
        }
    }

    internal void Pinch(float pinchDelta)
    {
        float pinchScaleFactor = 0.01f;
        float baseScale = 1f;

        if (selectedObject != null)
        {
            float scaleMultiplier = baseScale + pinchDelta * pinchScaleFactor;

            selectedObject.ProcessScale(scaleMultiplier);
        }
        else
        {
            float cameraZoomFactor = 0.1f;
            Camera.main.fieldOfView -= pinchDelta * cameraZoomFactor;
        }
    }

    internal void Rotate(float rotationDelta)
    {
        rotationSpeed = 2.0f;
        float rotation = rotationDelta * rotationSpeed;

        if (selectedObject != null)
        {
            
            selectedObject.ProcessRotation(rotation);
        }

        else
        {
            //rotationSpeed = .5f;
            //Camera.main.transform.Rotate(Vector3.up, rotation);
        }
    }

    private void SelectObject(IInteractable newObject)
    {
        DeselectObject();

        selectedObject = newObject;
        selectedObject.Select();
    }

    private void DeselectObject()
    {
        if (selectedObject != null)
        {
            selectedObject.DeSelect();
            selectedObject = null;
        }
    }
}
