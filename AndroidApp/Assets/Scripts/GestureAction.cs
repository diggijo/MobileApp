using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

public class GestureAction : MonoBehaviour
{
    private IInteractable selectedObject;
    [SerializeField] GameObject circle;
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

            objectHit.processTap();
        }
    }

    internal void drag(Vector2 position)
    {
        if(selectedObject != null)
        {
            Vector3 newPos = new Vector3(position.x, position.y, hitDistance);
            selectedObject.processDrag(newPos);
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
