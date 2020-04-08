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

    public void ApplyCurrentTetramino(ForecastShape _shape) {
        // Debug.Log("Applying!!!");
        int x = shapePositioning.currentOriginTile.x;
        int y = shapePositioning.currentOriginTile.y;
        ForecastType type = WeatherQueue.currentWeather;
        for (int i = 0; i < _shape.tiles.Count; i++) {
            Vector2Int offs = ShapePositioning.RotateOffset(_shape.tiles[i].offset, shapePositioning.rotation);
            // note that this is not using the type on the forecastTile (deprecated), but is instead using the current weather in the weatherqueue. 
            if(_shape.tiles[i].type != ForecastType.None) state.AddWeather(x+offs.x, y+offs.y, type, 1);
            // if(_shape.tiles[i].type == ForecastType.Sun) state.AddSunlight(x+offs.x, y+offs.y, 1);
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(PGApplyTetramino))]
public class PGApplyTetraminoEditor : Editor{
    SerializedProperty state;
    SerializedProperty targeting;
    SerializedProperty shapePositioning;
    private void OnEnable()
    {
        state = serializedObject.FindProperty("state");
        // targeting = serializedObject.FindProperty("targeting");
        shapePositioning = serializedObject.FindProperty("shapePositioning");
    }
    public override void OnInspectorGUI() {
        serializedObject.Update();
        GUI.enabled = false;
        EditorStyles.label.wordWrap = true;
        GUI.enabled = true;
        EditorGUILayout.PropertyField(state);
        EditorGUILayout.PropertyField(shapePositioning);
        serializedObject.ApplyModifiedProperties();
    }
}
#endif