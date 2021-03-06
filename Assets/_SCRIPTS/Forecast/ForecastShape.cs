﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ForecastShape
{
    [HideInInspector] public ForecastTile[] editorTiles = new ForecastTile[25]{new ForecastTile(0,0),new ForecastTile(0,1),new ForecastTile(0,2),new ForecastTile(0,3),new ForecastTile(0,4),new ForecastTile(1,0),new ForecastTile(1,1),new ForecastTile(1,2),new ForecastTile(1,3),new ForecastTile(1,4),new ForecastTile(2,0),new ForecastTile(2,1),new ForecastTile(2,2),new ForecastTile(2,3),new ForecastTile(2,4),new ForecastTile(3,0),new ForecastTile(3,1),new ForecastTile(3,2),new ForecastTile(3,3),new ForecastTile(3,4),new ForecastTile(4,0),new ForecastTile(4,1),new ForecastTile(4,2),new ForecastTile(4,3),new ForecastTile(4,4)};
    List<ForecastTile> _tiles;
    bool tilesInitialized = false;
    [HideInInspector] public List<ForecastTile> tiles {
        get{
            if (!tilesInitialized) {
                _tiles = new List<ForecastTile>();
                for ( int i = 0; i < editorTiles.Length; i++) {
                    if (!(editorTiles[i].type.Equals(ForecastType.None))) {
                        _tiles.Add(editorTiles[i]);
                    }
                }
                tilesInitialized = true;
            }
            return _tiles;
        }
    }
    [HideInInspector] public ForecastQueue forecastQueue;
}
