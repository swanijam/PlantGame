using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForecastShapeGroupTester : MonoBehaviour
{
    public ForecastShapeGroup forecastShapeGroup; 
    private void OnEnable()
    {
        for (int i = 0; i < forecastShapeGroup.shapes.Length; i++) {
            ForecastTile[] tiles = forecastShapeGroup.shapes[i].tiles.ToArray();
            for (int j = 0; j < tiles.Length; j++) {
                Debug.Log(tiles[j].offset +","+ tiles[j].editorOffset +","+ tiles[j].type);
            }
            // Debug.Log(tiles);
        }
    }
}
