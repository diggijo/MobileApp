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

    private enum CurrentAction
    {
        None,
        Drag,
        Rotate,
        Pinch,
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
        if (currentAction == CurrentAction.None || currentAction == CurrentAction.Pinch)
        {
            currentAction = CurrentAction.Rotate;
            OnRotate?.Invoke(touch);
        }
    }

    internal void SetPinch(Touch t1, Touch t2)
    {
        if (currentAction == CurrentAction.None || currentAction == CurrentAction.Rotate)
        {
            currentAction = CurrentAction.Pinch;
            OnPinch?.Invoke(Vector2.Distance(t1.position, t2.position));
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

                /*if (currentAction == CurrentAction.Rotate)
                {
                    OnRotate?.Invoke(touchHandler.RotationDelta);
                }*/

                if (currentAction == CurrentAction.Pinch)
                {
                    OnPinch?.Invoke(Vector2.Distance(touchHandler.firstTouch.position, touchHandler.secondTouch.position));
                }
            }
        }
    }
}
