using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void processDrag(Vector3 position);

    void select();

    void deSelect();
}
