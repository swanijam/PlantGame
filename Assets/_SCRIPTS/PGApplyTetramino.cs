using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PGApplyTetramino : MonoBehaviour
{
    public PGTileStateManager state;
    public PGTileTargeting targeting;
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
    float d_currentRotation = 0f;
    public static void ApplyCurrentTetramino(int x, int y, float rotation = 0f) {
        instance._ApplyCurrentTetramino(x,y,rotation);
    }
    public void _ApplyCurrentTetramino(int x, int y, float rotation = 0f) {
        ForecastShape _shape = currentShape;
        ForecastType type = WeatherQueue.currentWeather;
        for (int i = 0; i < _shape.tiles.Count; i++) {
            Vector2Int offs = RotateOffset(_shape.tiles[i].offset, rotation);
            if(_shape.tiles[i].type != ForecastType.None) state.AddWeather(x+offs.x, y+offs.y, type, 1);
            // if(_shape.tiles[i].type == ForecastType.Sun) state.AddSunlight(x+offs.x, y+offs.y, 1);
        }
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) {
            if (onTetraminoApplied != null) onTetraminoApplied();
            ApplyCurrentTetramino(targeting.currentTile.x, targeting.currentTile.y, d_currentRotation);
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            d_currentRotation += 90f;
        }
    }
}