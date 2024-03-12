using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    internal Dictionary<int, TouchHandler> touchHandlers = new Dictionary<int, TouchHandler>();
    public event Action<Touch> OnDrag;
    public event Action<float> OnRotate;
    public event Action<float> OnPinch;
    public event Action<Touch> OnTap;
    public event Action<Touch> OnSwipe;
    private float initialPinchDistance = 0;
    private float minPinchDistance = 2.5f;
    private float pinchDelta = 0;

    private enum CurrentAction
    {
        None,
        Drag,
        PinchRotate,
        Tap
    }

    private CurrentAction currentAction = CurrentAction.None;

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

        InvokeContinuousEvents();
        print(currentAction);
    }

    internal void RemoveTouch(int id)
    {
        touchHandlers.Remove(id);
        currentAction = CurrentAction.None;
    }

    internal void SetDrag(Touch touch)
    {
        if(currentAction == CurrentAction.None)
        {
            currentAction = CurrentAction.Drag;
            OnDrag?.Invoke(touch);
        }
    }

    internal void SetRotate(float touch)
    {
        if (currentAction == CurrentAction.None)
        {
            currentAction = CurrentAction.PinchRotate;
            OnRotate?.Invoke(touch);
        }
    }

    internal void SetPinch(Touch t1, Touch t2)
    {
        Vector2 touch1Pos = t1.position;
        Vector2 touch2Pos = t2.position;

        if (currentAction == CurrentAction.None)
        {
            currentAction = CurrentAction.PinchRotate;
            float currentPinchDistance = Vector2.Distance(touch1Pos, touch2Pos);

            if (initialPinchDistance == 0)
            {
                initialPinchDistance = currentPinchDistance;
            }

            else
            {
                pinchDelta = currentPinchDistance - initialPinchDistance;

                if (Mathf.Abs(pinchDelta) > minPinchDistance)
                {
                    OnPinch?.Invoke(pinchDelta);
                }

                initialPinchDistance = currentPinchDistance;
            }
        }
    }

    internal void SetTap(Touch touch)
    {
        if (currentAction == CurrentAction.None)
        {
            currentAction = CurrentAction.Tap;
            OnTap?.Invoke(touch);
        }
    }

    private void InvokeContinuousEvents()
    {
        foreach (var touchHandler in touchHandlers.Values)
        {
            if (touchHandler != null)
            {
                if (currentAction == CurrentAction.Drag)
                {
                    OnDrag?.Invoke(touchHandler.firstTouch);
                }

                if (currentAction == CurrentAction.PinchRotate)
                {
                    OnPinch?.Invoke(pinchDelta);
                    //OnRotate?.Invoke(touchHandler.RotationDelta);
                }
            }
        }
    }
}
