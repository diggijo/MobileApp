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
    private bool isPinching = false;
    private float minRotateAngle = .2f;
    private bool isRotating = false;
    private GestureAction actOn;
    private TouchManager myHandler;
    private float initialPinchDistance;
    private Vector2 initialRotationVector = new Vector2(0, 0);

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
                    
                    if(!isPinching && !isRotating)
                    {
                        actOn.Drag(t);
                    }
                }

                if (Input.touchCount >= 2)
                {
                    for (int i = 0; i < Input.touchCount; i++)
                    {
                        Touch currentTouch = Input.GetTouch(i);

                        // Scale (Pinch)
                        if (i < Input.touchCount - 1)
                        {
                            Vector2 touch1 = currentTouch.position;
                            Vector2 touch2 = Input.GetTouch(i + 1).position;

                            float currentPinchDistance = Vector2.Distance(touch1, touch2);

                            if (initialPinchDistance == 0)
                            {
                                initialPinchDistance = currentPinchDistance;
                            }

                            else
                            {
                                float pinchDelta = currentPinchDistance - initialPinchDistance;

                                if (Mathf.Abs(pinchDelta) > minPinchDistance)
                                {
                                    isPinching = true;
                                    actOn.Pinch(pinchDelta);
                                }

                                else
                                {
                                    isPinching = false;
                                }

                                initialPinchDistance = currentPinchDistance;
                            }
                        }

                        // Rotation
                        if (i < Input.touchCount - 1)
                        {
                            Vector2 currentRotationVector = Input.GetTouch(i + 1).position;
                            float rotationDelta = Vector2.Angle(initialRotationVector, currentRotationVector);

                            Vector3 cross = Vector3.Cross(initialRotationVector, currentRotationVector);

                            if (cross.z > 0)
                            {
                                rotationDelta = -rotationDelta;
                            }

                            if (Mathf.Abs(rotationDelta) > minRotateAngle)
                            {
                                isRotating = true;
                                actOn.Rotate(rotationDelta);
                            }
                            else
                            {
                                isRotating = false;
                            }

                            initialRotationVector = currentRotationVector;
                        }
                    }
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
                        actOn.TapAt(t);
                    }
                }

                Destroy(gameObject);
                myHandler.RemoveTouch(t.fingerId);

                break;
        }
    }
}
