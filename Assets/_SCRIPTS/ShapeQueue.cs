﻿using System.Collections;
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
        for (int i = 0; i < numDays; i++) {
            int selection = Random.Range(0, forecastShapes.shapes.Length);
            shapeQueue.Add(forecastShapes.shapes[selection]);
        }
        // Debug.Log("Initialized Weather Queue");
    }

    public void AdvanceQueue() {
        shapeQueue.RemoveAt(0);
    }

    public int _selectedShape = -1;
    public static int selectedShape {
        get { return instance._selectedShape; }
    }
    public static void SelectShape(int index) {
        instance._selectedShape = index;
    }
}