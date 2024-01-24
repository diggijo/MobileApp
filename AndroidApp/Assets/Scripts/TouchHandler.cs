using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchHandler : MonoBehaviour
{
    private TouchManager touchManager;
    private int touchCount;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch t in Input.touches)
            {
                if(t.phase == TouchPhase.Began)
                {
                    touchCount++;
                    GameObject newTouch = new GameObject("NewTouch" + touchCount.ToString());
                    touchManager = newTouch.AddComponent<TouchManager>();
                }

                touchManager.HandleTouch(t);
            }
        }
    }
}
