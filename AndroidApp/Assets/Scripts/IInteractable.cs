using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void processTap();

    void processDrag(Vector3 position);

    void selectObject(GameObject selectedObject);
    void deselectObject(GameObject selectedObject);
}
