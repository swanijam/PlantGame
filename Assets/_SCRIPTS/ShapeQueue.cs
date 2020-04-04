using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeQueue : MonoBehaviour
{
    public static ShapeQueue instance;
    private void Awake()
    {
        if (instance != null) {
            GameObject.DestroyImmediate(instance.gameObject);
            Debug.LogError("additional weatherqueue found. deleting the older one");
        }
        instance = this;
        // Debug.Log("defined instance");
    }
    public List<ForecastShape> shapeQueue;
    public static ForecastShape currentWeather {
        get {
            return instance.shapeQueue[0];
        }
    }

    public static ForecastShape GetShape(int index) {
        return instance.shapeQueue.ToArray()[index];
    }

    public static ForecastShape[] GetShapeArray() {
        return instance.shapeQueue.ToArray();
    }

    public ForecastShapeGroup forecastShapes;
    public void Initialize(int numDays=10) {
        shapeQueue = new List<ForecastShape>();
        int previous = -1;
        for (int i = 0; i < numDays; i++) {
            int selection = Random.Range(0, forecastShapes.shapes.Length);
            while (selection == previous) {
                selection = Random.Range(0, forecastShapes.shapes.Length);
            }
            previous = selection;
            shapeQueue.Add(forecastShapes.shapes[selection]);
        }
    }

    public void AdvanceQueue() {
        shapeQueue.RemoveAt(0);
    }

    public void RemoveSelectedShape() {
        shapeQueue.RemoveAt(selectedShape);
        _selectedShape = -1;
    }

    public int _selectedShape = -1;
    public static int selectedShape {
        get { return instance._selectedShape; }
    }
    public static void SelectShape(int index) {
        instance._selectedShape = index;
        Debug.Log(index);
        ShapePositioning.currentShape = instance.shapeQueue[index];
        ShapePositioning.instance.BuildTileArray(); // maybe do this within the turn action? not sure
    }
    public static void ConsumeShape() {
        instance.shapeQueue.RemoveAt(instance._selectedShape);
    }
}
