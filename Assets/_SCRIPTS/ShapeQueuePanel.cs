using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeQueuePanel : MonoBehaviour
{
    public GameObject shapeButtonPrefab;
    public RectTransform shapeGroup;

    public void Initialize() {
        ForecastShape[] shape = ShapeQueue.GetShapeArray();
        GameObject newGo;
        for (int i = 0; i < shape.Length; i++) {
            // build a fancy UI representing the shape
            newGo = Instantiate(shapeButtonPrefab, shapeGroup.position, Quaternion.identity, shapeGroup);
            newGo.GetComponent<SelectShape>().shapeIndexInQueue = i;
            newGo.GetComponent<ShapeImageAssembler>().shape = shape[i];
            newGo.GetComponent<ShapeImageAssembler>().UpdateVisual();
            newGo.SetActive(true);
        }
        // Debug.Log("Initialized Weather Queue Panel");
    }
}
