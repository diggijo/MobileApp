using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

public class GestureAction : MonoBehaviour
{
    [SerializeField] GameObject circle;
    private float zPos = 10f;
    private float hitDistance;
    private Vector3 offset;


    
    internal void tapAt(Vector2 position)
    {
        createCircle(position);

        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo))
        {
            IInteractable objectHit = hitInfo.collider.gameObject.GetComponent<IInteractable>();
            objectHit.processTap();
        }
    }

    internal void drag(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            IInteractable objectHit = hitInfo.collider.gameObject.GetComponent<IInteractable>();
            hitDistance = Vector3.Distance(hitInfo.transform.position, Camera.main.transform.position);
            Vector3 newPos = new Vector3(position.x, position.y, hitDistance);
            objectHit.processDrag(newPos);
        }
    }

    internal GameObject createCircle(Vector2 position)
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, zPos));
        GameObject circleGO = Instantiate(circle, worldPos, Quaternion.identity);

        circleGO.SetActive(true);

        return circleGO;
    }
}
