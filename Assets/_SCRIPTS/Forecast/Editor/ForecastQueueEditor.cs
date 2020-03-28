using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(ForecastQueue), true)]
public class ForecastQueueEditor : UnityEditor.Editor
{
    private ReorderableList _myList;

    private void OnEnable()
    {
        ForecastQueue forecastQueue = target as ForecastQueue;
        // _myList = new ReorderableList(serializedObject, serializedObject.FindProperty("forecastShapes"), true, true, true, true);
        if (forecastQueue != null)
        {
            _myList = new ReorderableList(forecastQueue.forecastShapes, typeof(ForecastShape), true, true, true, true);
            _myList.drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, "Forecast Shapes", EditorStyles.boldLabel);
            };

            _myList.onAddCallback = (_myList) =>
                {
                    forecastQueue.forecastShapes.Add(new ForecastShape());
                };

            _myList.onRemoveCallback = (_myList) =>
                {
                    forecastQueue.forecastShapes.RemoveAt(_myList.index);
                };

            _myList.drawElementCallback =
                (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    var oldColor = GUI.backgroundColor;
                    GUI.backgroundColor = Color.red;

                    var element = forecastQueue.forecastShapes[index];
                    EditorGUI.LabelField(rect, "Setting " + index, EditorStyles.boldLabel);

                    for (int x = 0; x < 5; x++)
                    {
                        for (int y = 0; y < 5; y++)
                        {
                            for (int i = 0; i < forecastQueue.forecastShapes[index].editorTiles.Length; i++)
                            {
                                if (forecastQueue.forecastShapes[index].editorTiles[x * 5 + y].editorOffset.x == x && forecastQueue.forecastShapes[index].editorTiles[x * 5 + y].editorOffset.y == y)
                                {
                                    if (forecastQueue.forecastShapes[index].editorTiles[x * 5 + y].type == ForecastType.Water)
                                    {
                                        GUI.backgroundColor = Color.blue;
                                    }
                                    else if (forecastQueue.forecastShapes[index].editorTiles[x * 5 + y].type == ForecastType.Sun)
                                    {
                                        GUI.backgroundColor = Color.yellow;
                                    }
                                    else
                                    {
                                        GUI.backgroundColor = oldColor;
                                    }
                                }
                            }
                            if (GUI.Button(new Rect(rect.x + x * 20, rect.y + 100 - (y+1) * 20 + EditorGUIUtility.singleLineHeight/2, 20, 20), ""))
                            {
                                forecastQueue.forecastShapes[index].editorTiles[x * 5 + y].type = forecastQueue.PaintMode;
                                fillTileArray(ref forecastQueue, index);
                                centerAtOrigin(ref forecastQueue, index);
                            }
                        }
                    }
                    GUI.backgroundColor = oldColor;
                };

            _myList.elementHeight = (EditorGUIUtility.singleLineHeight * 6 + 10 + EditorGUIUtility.standardVerticalSpacing);

        }
    }

    public override void OnInspectorGUI()
    {

        ForecastQueue forecastQueue = target as ForecastQueue;

        serializedObject.Update();
        if (_myList != null)
            _myList.DoLayoutList();


        forecastQueue.PaintMode = (ForecastType)EditorGUILayout.EnumPopup("Paint Mode", forecastQueue.PaintMode);

        serializedObject.ApplyModifiedProperties();

        DrawDefaultInspector();
    }

    public void fillTileArray(ref ForecastQueue forecastQueue, int index)
    {
        // forecastQueue.forecastShapes[index].tiles.Clear();
        // for (int x = 0; x < 5; x++)
        // {
        //     for (int y = 0; y < 5; y++)
        //     {
        //         if (forecastQueue.forecastShapes[index].editorTiles[x * 5 + y].type != ForecastType.None)
        //         {
        //             forecastQueue.forecastShapes[index].tiles.Add(forecastQueue.forecastShapes[index].editorTiles[x * 5 + y]);
        //         }
        //     }
        // }
    }

    public void centerAtOrigin(ref ForecastQueue forecastQueue, int index)
    {


        int xMin = 5;
        int yMin = 5;
        ForecastTile[] tiles = forecastQueue.forecastShapes[index].tiles.ToArray();
        for (int j = 0; j < tiles.Length; j++)
        {
            if (tiles[j].editorOffset.x < xMin) xMin = tiles[j].editorOffset.x;
            if (tiles[j].editorOffset.y < yMin) yMin = tiles[j].editorOffset.y;
        }
        for (int j = 0; j < tiles.Length; j++)
        {
            tiles[j].offset.x = tiles[j].editorOffset.x - xMin;
            tiles[j].offset.y = tiles[j].editorOffset.y - yMin;
        }
    }
}
