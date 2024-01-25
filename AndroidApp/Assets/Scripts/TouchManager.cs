using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private float touchTimer = 0;
    private float moveTimer = 0;
    private bool hasMoved = false;
    private float maxTapTime = 0.25f;
    private GestureAction actOn;

    void Start()
    {
        actOn = FindObjectOfType<GestureAction>();

        if (actOn == null)
        {
            Debug.LogError("GestureAction not found in the scene.");
        }
    }

    public void HandleTouch(Touch t)
    {
        switch (t.phase)
        {
            case TouchPhase.Began:

                touchTimer = 0f;
                moveTimer = 0f;

                break;

            case TouchPhase.Moved:

                moveTimer += Time.deltaTime;

                if(moveTimer > maxTapTime)
                {
                    hasMoved = true;
                    actOn.drag(t.position);
                }

                break;

            case TouchPhase.Stationary:

                touchTimer += Time.deltaTime;

                break;

            case TouchPhase.Ended:

                if(!hasMoved)
                {
                    if (touchTimer < maxTapTime)
                    {
                        actOn.tapAt(t.position);
                    }
                }

                break;
        }
    }
}
