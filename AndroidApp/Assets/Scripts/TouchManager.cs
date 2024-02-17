using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    internal Dictionary<int, TouchHandler> touchHandlers = new Dictionary<int, TouchHandler>();

    void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch t in Input.touches)
            {
                if (t.phase == TouchPhase.Began)
                {
                    GameObject newTouchObject = new GameObject("NewTouch" + t.fingerId.ToString());
                    TouchHandler touchHandler = newTouchObject.AddComponent<TouchHandler>();
                    touchHandler.introductions(this);

                    touchHandlers.Add(t.fingerId, touchHandler);
                }

                touchHandlers[t.fingerId].HandleTouch(t);
            }
        }
    }

    internal void RemoveTouch(int id)
    {
        touchHandlers.Remove(id);
    }
}
