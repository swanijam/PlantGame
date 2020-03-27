using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectShape : MonoBehaviour
{
    public int shapeIndexInQueue = 0;
    public void SelectShapeNow() {
        ShapeQueue.SelectShape(shapeIndexInQueue);
    }
}
