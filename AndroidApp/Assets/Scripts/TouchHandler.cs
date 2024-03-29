using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class TouchHandler : MonoBehaviour
{
    private float touchTimer = 0;
    private float moveTimer = 0;
    private bool hasMoved = false;
    private float maxTapTime = 0.25f;
    private float minPinchDistance = 2.5f;
    private float minRotateAngle = .1f;
    private float maxRotateAngle = 10f;
    private GestureAction actOn;
    private TouchManager myHandler;
    private float initialPinchDistance;
    private Vector2 initialRotationVector = new Vector2(0, 0);
    internal Touch firstTouch;
    internal Touch secondTouch;

    void Start()
    {
        actOn = FindObjectOfType<GestureAction>();

        if (actOn == null)
        {
            Debug.LogError("GestureAction not found in the scene.");
        }
    }

    internal void introductions(TouchManager tH)
    {
        myHandler = tH;
    }

    public void HandleTouch(Touch t)
    {
        firstTouch = t;

        switch (t.phase)
        {
            case TouchPhase.Began:

                hasMoved = false;
                touchTimer = 0f;
                moveTimer = 0f;
                initialPinchDistance = 0;
                initialRotationVector = t.position;

                break;

            case TouchPhase.Moved:

                moveTimer += Time.deltaTime;

                if (moveTimer > maxTapTime)
                {
                    hasMoved = true;
                    
                    if(Input.touchCount == 1)
                    {
                        print(myHandler.touchHandlers[t.fingerId]);
                        myHandler.SetDrag(t);
                    }
                }

                else
                {
                    actOn.Swipe(t);
                }

                if (Input.touchCount == 2)
                {
                    firstTouch = Input.GetTouch(0);
                    secondTouch = Input.GetTouch(1);

                    myHandler.SetPinch(firstTouch, secondTouch);



                    /*Vector2 touch1Pos = Input.GetTouch(0).position;
                    Vector2 touch2Pos = Input.GetTouch(1).position;

                    //Pinch
                    float currentPinchDistance = Vector2.Distance(touch1Pos, touch2Pos);

                    if (initialPinchDistance == 0)
                    {
                        initialPinchDistance = currentPinchDistance;
                    }

                    else
                    {
                        float pinchDelta = currentPinchDistance - initialPinchDistance;

                        if (Mathf.Abs(pinchDelta) > minPinchDistance)
                        {
                            myHandler.SetPinch(pinchDelta);
                        }

                        initialPinchDistance = currentPinchDistance;
                    }


                    //Rotation
                    Vector2 currentRotationVector = touch2Pos;

                    float rotationDelta = Vector2.Angle(initialRotationVector, currentRotationVector);

                    Vector3 cross = Vector3.Cross(initialRotationVector, currentRotationVector);

                    if (cross.z > 0)
                    {
                       rotationDelta = -rotationDelta;
                    }

                    if (Mathf.Abs(rotationDelta) > minRotateAngle && Mathf.Abs(rotationDelta) < maxRotateAngle)
                    {
                        myHandler.SetRotate(rotationDelta);
                    }

                    initialRotationVector = currentRotationVector;*/
                }

                break;

            case TouchPhase.Stationary:

                touchTimer += Time.deltaTime;

                break;

            case TouchPhase.Ended:

                if (!hasMoved)
                {
                    if (touchTimer < maxTapTime)
                    {
                        myHandler.SetTap(t);
                    }
                }

                Destroy(gameObject);
                myHandler.RemoveTouch(t.fingerId);

                break;
        }
    }

}
