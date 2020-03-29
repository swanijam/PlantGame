using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
// using UnityEditorInternal;  // contains reorderable list

[CustomPropertyDrawer(typeof(ForecastShape), true)]
public class ForecastShapePD: PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return 20 * 5 + EditorGUIUtility.singleLineHeight/2;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var oldColor = GUI.backgroundColor;
        Rect boxRect = new Rect(position.x,position.y+5,20,20);
        SerializedProperty editorTiles = property.FindPropertyRelative("editorTiles"); 
        if (editorTiles.arraySize == 0) return;
        for (int x = 0; x < 5; x++) {
            for (int y = 0; y < 5; y++) {
                SerializedProperty editorTile = editorTiles.GetArrayElementAtIndex(x*5+y);
                Vector2Int editorOffset = editorTile.FindPropertyRelative("editorOffset").vector2IntValue;
                // for (int i = 0; i < count; i++) {
                ForecastType type = (ForecastType)(editorTile.FindPropertyRelative("type").enumValueIndex);
                if (editorOffset.x == x && editorOffset.y == y) { // get the relevant forecastTie to test its type
                    if (type.Equals(ForecastType.Water))
                    {
                        GUI.backgroundColor = Color.blue;
                    }
                    else if (type.Equals(ForecastType.Sun))
                    {
                        GUI.backgroundColor = Color.yellow;
                    }
                    else
                    {
                        GUI.backgroundColor = oldColor;
                    }
                }
                if (GUI.Button(new Rect(position.x + x * 20, position.y + 100 - (y+1) * 20 + EditorGUIUtility.singleLineHeight/2, 20, 20), ""))
                {
                    SerializedProperty editorTileType = editorTile.FindPropertyRelative("type");
                    editorTileType.enumValueIndex = (editorTileType.enumValueIndex + 1) % 3;
                    centerAtOrigin(editorTiles);
                }
            }
        }
        GUI.backgroundColor = oldColor;
        property.serializedObject.ApplyModifiedProperties();
        // Debug.Log(debugString);
    }
    public void centerAtOrigin(SerializedProperty editorTiles)
    {
        int xMin = 5;
        int yMin = 5;
        int xMax = -1;
        int yMax = -1;
        int xMid = 2;
        int yMid = 2;        
        
        for (int j = 0; j < editorTiles.arraySize; j++)
        {
            Vector2Int editorOffset = editorTiles.GetArrayElementAtIndex(j).FindPropertyRelative("editorOffset").vector2IntValue;
            ForecastType type = (ForecastType)(editorTiles.GetArrayElementAtIndex(j).FindPropertyRelative("type").enumValueIndex);
            if (type != 0) { // find the non-null tiles and get the minimum coordinates
                if (editorOffset.x < xMin) xMin = editorOffset.x;
                if (editorOffset.y < yMin) yMin = editorOffset.y;
                if (editorOffset.x > xMax) xMax = editorOffset.x;
                if (editorOffset.y > yMax) yMax = editorOffset.y;
            }
        }
        xMid = (int)(((float)xMax-(float)xMin)/2f +(float)xMin + .5f);
        yMid = (int)(((float)yMax-(float)yMin)/2f +(float)yMin + .5f);
        // Debug.Log(xMid + "," + xMin + "," + xMax);
        for (int j = 0; j < editorTiles.arraySize; j++)
        {
            Vector2Int editorOffset = editorTiles.GetArrayElementAtIndex(j).FindPropertyRelative("editorOffset").vector2IntValue;
            Vector2Int offset = editorTiles.GetArrayElementAtIndex(j).FindPropertyRelative("offset").vector2IntValue;
            offset.x = editorOffset.x - xMid;
            offset.y = editorOffset.y - yMid;
            // Debug.Log(editorOffset + "/" + offset);
            editorTiles.GetArrayElementAtIndex(j).FindPropertyRelative("offset").vector2IntValue = offset;
        }
        editorTiles.serializedObject.ApplyModifiedProperties();
    }
}
