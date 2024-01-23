using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private float touchTimer = 0;
    private bool hasMoved = false;
    private float MaxTapTime = 1f;
    private GestureAction actOn;

    void Start()
    {
        actOn = FindObjectOfType<GestureAction>();
    }

    void Update()
    {
        if(Input.touchCount > 0)
        {
            foreach (Touch t in Input.touches)
            {
                switch (t.phase)
                {
                    case TouchPhase.Began:

                        touchTimer = 0f;

                        break;
                    case TouchPhase.Moved:

                        hasMoved = true;

                        //actOn.drag(t.position);

                        break;
                    case TouchPhase.Stationary:

                        touchTimer += Time.deltaTime;

                        break;
                    case TouchPhase.Ended:
                        hasMoved = false;

                        if (touchTimer < MaxTapTime && !hasMoved)
                        {
                            actOn.tapAt(t.position);
                        }
                        break;
                }
            }
        }
    }
}
