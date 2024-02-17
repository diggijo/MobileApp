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
    private GestureAction actOn;
    private TouchManager myHandler;
    private float initialPinchDistance;
    private Vector2 initialRotationVector;

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
                    actOn.drag(t);
                }

                if (Input.touchCount >= 2)
                {
                    Vector2 touch1 = Input.GetTouch(0).position;
                    Vector2 touch2 = Input.GetTouch(1).position;

                    float currentPinchDistance = Vector2.Distance(touch1, touch2);

                    if (initialPinchDistance == 0)
                    {
                        initialPinchDistance = currentPinchDistance;
                    }

                    else
                    {
                        float pinchDelta = currentPinchDistance - initialPinchDistance;

                        actOn.pinch(pinchDelta);

                        initialPinchDistance = currentPinchDistance;
                    }
                }

                if (Input.touchCount == 2)
                {
                    Vector2 currentRotationVector = Input.GetTouch(1).position;
                    float rotationDelta = Vector2.Angle(initialRotationVector, currentRotationVector);

                    Vector3 cross = Vector3.Cross(initialRotationVector, currentRotationVector);

                    if (cross.z < 0)
                    {
                        rotationDelta = -rotationDelta;
                    }

                    actOn.rotate(rotationDelta);

                    initialRotationVector = currentRotationVector;
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
                        actOn.tapAt(t);
                    }
                }

                Destroy(gameObject);
                myHandler.removeTouch(t.fingerId);

                break;
        }
    }
}
