using System;
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
    Touch myTouch;
    TouchHandler myHandler;

    void Start()
    {
        actOn = FindObjectOfType<GestureAction>();

        if (actOn == null)
        {
            Debug.LogError("GestureAction not found in the scene.");
        }
    }

    public void HandleTouch()
    {
        print("2");
        switch (myTouch.phase)
        {
            case TouchPhase.Began:
                print("3");
                hasMoved = false;
                touchTimer = 0f;
                moveTimer = 0f;
                //actOn.selectObject(myTouch.position);

                break;

            case TouchPhase.Moved:
                print("4");
                moveTimer += Time.deltaTime;

                if(moveTimer > maxTapTime)
                {
                    hasMoved = true;
                    actOn.drag(myTouch.position);
                }

                break;

            case TouchPhase.Stationary:
                print("5");
                touchTimer += Time.deltaTime;

                break;

            case TouchPhase.Ended:
                print("6");
                if (!hasMoved)
                {
                    if (touchTimer < maxTapTime)
                    {
                        actOn.tapAt(myTouch.position);
                    }


                }
                print("Removing");
                myHandler.ImGone(this);
                Destroy(gameObject);
                break;
        }
    }


    private void Update()
    {
        print("1");
        HandleTouch();
    }
    internal void introductions(Touch t, TouchHandler touchHandler)
    {
       myTouch = t;
       myHandler = touchHandler;
    }
}
