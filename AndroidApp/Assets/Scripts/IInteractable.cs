using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void ProcessDrag(Vector3 position, string dragType);

    void ProcessScale(float scaleMultiplier);

    void ProcessRotation(float rotation);
    void Select();

    void DeSelect();
}
