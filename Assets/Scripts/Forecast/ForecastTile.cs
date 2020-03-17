using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ForecastTile
{
    public Vector2Int offset;
    public Vector2Int editorOffset;
    public ForecastType type = ForecastType.None;

    public ForecastTile(int x, int y)
    {
        editorOffset.x = x;
        editorOffset.y = y;
    }
}
