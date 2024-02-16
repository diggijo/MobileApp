using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void processDrag(Vector3 position, string dragType);

    void select();

    void deSelect();
}
