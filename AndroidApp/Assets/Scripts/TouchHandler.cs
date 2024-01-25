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
                if (t.phase == TouchPhase.Began)
                {
                    touchCount++;
                    GameObject newTouchObject = new GameObject("NewTouch" + touchCount.ToString());
                    touchManager = newTouchObject.AddComponent<TouchManager>();
                }

                touchManager.HandleTouch(t);
            }
        }
    }
}
