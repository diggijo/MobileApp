using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchHandler : MonoBehaviour
{
   // private Dictionary<int, TouchManager> touchManagers = new Dictionary<int, TouchManager>();
    List<TouchManager> touchesAsGO;
    TouchManager newTouch;

    internal void ImGone(TouchManager touchManager)
    {
       touchesAsGO.Remove(touchManager);
    }

    private void Start()
    {
        touchesAsGO = new List<TouchManager>();
    }
    void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch t in Input.touches)
            {
                if (t.phase == TouchPhase.Began)
                {
                    GameObject newTouchObject = new GameObject("NewTouch" + t.fingerId.ToString());
                    newTouch = newTouchObject.AddComponent<TouchManager>();
                    touchesAsGO.Add(newTouch);
                }

                newTouch.introductions(t, this);

                // touchManagers[t.fingerId].HandleTouch(t);

                //if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
                //{
                //    Destroy(touchManagers[t.fingerId].gameObject);
                //    touchManagers.Remove(t.fingerId);
                //}
            }
        }
    }
}
