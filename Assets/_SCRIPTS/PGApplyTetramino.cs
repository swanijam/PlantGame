using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PGApplyTetramino : MonoBehaviour
{
    public PGTileStateManager state;
    public PGTileTargeting targeting;
    public ShapePositioning shapePositioning;
    // public ForecastQueue forecastQueue;
    public ForecastShape _currentShape;
    public static ForecastShape currentShape {
        get {
            return instance._currentShape;
        }
        set {
            instance._currentShape = value;
        }
    }

    public static PGApplyTetramino instance;
    private void Awake()
    {
        if (instance != null) {
            GameObject.Destroy(instance);
            Debug.Log("additional PGApplyTetramino isntance found. Deleting the old one");
        }
        instance = this;
    }


    
    // public Tetramino d_activeTetramino;
    float currentRotation = 0f;
    public static void ApplyCurrentTetramino() {
        instance._ApplyCurrentTetramino();
    }
    public void _ApplyCurrentTetramino() {
        int x = instance.targeting.currentTile.x;
        int y = instance.targeting.currentTile.y;
        float rotation =- currentRotation;
        ForecastShape _shape = currentShape;
        ForecastType type = WeatherQueue.currentWeather;
        for (int i = 0; i < _shape.tiles.Count; i++) {
            Vector2Int offs = RotateOffset(_shape.tiles[i].offset, rotation);
            if(_shape.tiles[i].type != ForecastType.None) state.AddWeather(x+offs.x, y+offs.y, type, 1);
            // if(_shape.tiles[i].type == ForecastType.Sun) state.AddSunlight(x+offs.x, y+offs.y, 1);
        }
    }
    private void Rotate() {
        currentRotation += 90f;
        shapePositioning.UpdateTileArray(Vector2Int.zero, currentRotation);
    }
    private Vector2Int RotateOffset(Vector2Int offset, float rotation) {
        float normalizedRotation = rotation % 360f;
        if (normalizedRotation < 0f) normalizedRotation += 360f;
        if (normalizedRotation < 90f) return new Vector2Int(-offset.y, offset.x);
        if (normalizedRotation < 180f) return new Vector2Int(-offset.x, -offset.y);
        if (normalizedRotation < 270f) return new Vector2Int(offset.y, -offset.x);
        // if (normalizedRotation < 360f) return offset;
        else return offset; // same thing as above comment, but satisfies "all code paths return a value"
    }

    public delegate void TetraminoApplied();
    public event TetraminoApplied onTetraminoApplied; 

    public static float PLACE_TILE_TIME = 2f; 
    public static float ROTATE_TIME = 1f;
    public static float DRAG_DISTANCE = 10f;
    float pressTime = 0f;
    bool blockUntilMouseUp = false;
    Vector3 dragOrigin;
    public float Delta() {
        float delta = (Input.mousePosition - dragOrigin).magnitude;
        return delta;
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            dragOrigin = Input.mousePosition;
        }
     
        if (!blockUntilMouseUp && Input.GetMouseButton(0)) {
            pressTime += Time.deltaTime;
            if (pressTime >= PLACE_TILE_TIME && Delta() < DRAG_DISTANCE) {
                blockUntilMouseUp = true;
                ApplyCurrentTetramino();
            }
        }

        if (Input.GetMouseButtonUp(0) && Delta() < DRAG_DISTANCE) {
            blockUntilMouseUp = false;
            if (pressTime <= ROTATE_TIME) {
                Rotate();
            }
        }
    }
}