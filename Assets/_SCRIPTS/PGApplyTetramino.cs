using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PGApplyTetramino : MonoBehaviour
{
    public PGTileStateManager state;
    public ShapePositioning shapePositioning;
    // public ForecastQueue forecastQueue;
    [Header("shape comes from shape queue selection")]
    public ForecastShape currentShape;

    public void ApplyCurrentTetramino() {
        int x = shapePositioning.currentOriginTile.x;
        int y = shapePositioning.currentOriginTile.y;
        ForecastShape _shape = currentShape;
        ForecastType type = WeatherQueue.currentWeather;
        for (int i = 0; i < _shape.tiles.Count; i++) {
            Vector2Int offs = shapePositioning.RotateOffset(_shape.tiles[i].offset, shapePositioning.rotation);
            if(_shape.tiles[i].type != ForecastType.None) state.AddWeather(x+offs.x, y+offs.y, type, 1);
            // if(_shape.tiles[i].type == ForecastType.Sun) state.AddSunlight(x+offs.x, y+offs.y, 1);
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(PGApplyTetramino))]
public class PGApplyTetraminoEditor : Editor{
    SerializedProperty currentShape;
    SerializedProperty state;
    SerializedProperty targeting;
    SerializedProperty shapePositioning;
    private void OnEnable()
    {
        currentShape = serializedObject.FindProperty("currentShape");
        state = serializedObject.FindProperty("state");
        // targeting = serializedObject.FindProperty("targeting");
        shapePositioning = serializedObject.FindProperty("shapePositioning");
    }
    public override void OnInspectorGUI() {
        serializedObject.Update();
        GUI.enabled = false;
        EditorStyles.label.wordWrap = true;
        EditorGUILayout.LabelField("the shape used by PGApplyTetramino comes directly from ShapePositioning, which comes from shapequeue selection");
        EditorGUILayout.PropertyField(currentShape);
        GUI.enabled = true;
        EditorGUILayout.PropertyField(state);
        EditorGUILayout.PropertyField(shapePositioning);
        serializedObject.ApplyModifiedProperties();
    }
}
#endif