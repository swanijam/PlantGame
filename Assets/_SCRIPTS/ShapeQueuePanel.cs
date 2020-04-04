using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeQueuePanel : MonoBehaviour
{
    public GameObject shapeButtonPrefab;
    public RectTransform shapeGroup;
    List<RectTransform> elements;
    public void Initialize() {
        ForecastShape[] shape = ShapeQueue.GetShapeArray();
        elements = new List<RectTransform>();
        GameObject newGo;
        for (int i = 0; i < shape.Length; i++) {
            // build a fancy UI representing the shape
            newGo = Instantiate(shapeButtonPrefab, shapeGroup.position, Quaternion.identity, shapeGroup);
            newGo.GetComponent<SelectShape>().shapeIndexInQueue = i;
            newGo.GetComponent<ShapeImageAssembler>().shape = shape[i];
            newGo.GetComponent<ShapeImageAssembler>().UpdateVisual();
            newGo.SetActive(true);
            elements.Add(newGo.GetComponent<RectTransform>());
        }
        // Debug.Log("Initialized Weather Queue Panel");
    }

    public void RemoveElement(int index) {
        StartCoroutine(_RemoveElement(index));
    }
    public AnimationCurve ScaleDownCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    public float ScaleDownTime = 1f;
    private IEnumerator _RemoveElement(int index) {
        RectTransform rt = elements[index];
        Vector2 startSize = rt.sizeDelta;
        float currTime = 0f;
        float lerpVal;
        WaitForEndOfFrame wfeof = new WaitForEndOfFrame();
        while (currTime < ScaleDownTime) {
            currTime += Time.deltaTime;
            lerpVal = 1f-ScaleDownCurve.Evaluate(Mathf.InverseLerp(0f, ScaleDownTime, currTime));
            rt.sizeDelta = startSize * lerpVal;
            yield return wfeof;
        }
        GameObject.Destroy(rt.gameObject);
        elements.RemoveAt(index);
        UpdateButtonSelectionIndices();
    }
    public void UpdateButtonSelectionIndices() {
        for (int i = 0; i < elements.Count; i++) {
            SelectShape selector = elements[i].gameObject.GetComponent<SelectShape>();
            selector.shapeIndexInQueue = i;
        }
    }
}
