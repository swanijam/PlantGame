using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectShape : MonoBehaviour
{
    public float currentRotation = 0f;
    public int shapeIndexInQueue = 0;
    public Transform rotationTransform;
    public void SelectShapeNow() {
        if (!selectPrepared) {
            return;
        }
        ShapeQueue.SelectShape(shapeIndexInQueue, currentRotation);
        selectPrepared = false;
    }

    public void RotateShape() {
        currentRotation += 90f;
        currentRotation = currentRotation % 360f;
        rotationTransform.localRotation = Quaternion.Euler(0f, 0f, currentRotation);
    }

    bool selectPrepared = false;
    public void PrepareSelect() {
        selectPrepared = true;
    }
}
