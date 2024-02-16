using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void processDrag(Vector3 position, string dragType);

    void processScale(float scaleMultiplier);
    void select();

    void deSelect();
}
