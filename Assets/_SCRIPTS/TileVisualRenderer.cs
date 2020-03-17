using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileVisualRenderer : MonoBehaviour
{
    public TileStateManager state;
    public Transform origin;
    public float tileWidth = 1;
    public float heightOffset;
    public GameObject renderGroupPrefab;
    public GameObject SoilRenderPrefab;
    
    [HideInInspector]
    public TileRenderGroup[,] tiles;
    
    public void Initialize() {
        tiles = new TileRenderGroup[state.dimensions.x, state.dimensions.y];
        for (int x = 0; x < state.dimensions.x; x++ ) {
            for (int y = 0; y < state.dimensions.y; y++ ) {
                // add a tile render group instance to the center of the cell
                TileRenderGroup newtrg = Instantiate(renderGroupPrefab, GetPosition(x,y, new Vector2(0.5f, 0.5f) ), Quaternion.identity, transform).GetComponent<TileRenderGroup>();
                // add a dirt prefab to every tile by default.
                if (SoilRenderPrefab != null) newtrg.visualizers.Add(Instantiate(SoilRenderPrefab, GetPosition(x,y, SoilRenderPrefab.GetComponent<Visualizer>().anchor), Quaternion.identity, newtrg.transform).GetComponent<Visualizer>());
                // subscribe to stateChange events
                state.tiles[x,y].onStateChanged += newtrg.OnTileStateChanged;
                newtrg.InitializeState(state.tiles[x,y].sunlightLevel, state.tiles[x,y].waterLevel);
                tiles[x,y] = newtrg;
            }
        }
    }
    
    // given a grid coordinate, returns position of tile + anchor*tileWidth.
    // so GetPosition(2,3, (.5f, .5f)) return the center of the tile in the 3rd row and 4th column.
    public Vector3 GetPosition(int tilex, int tiley, Vector2 anchor) {
        return origin.position + new Vector3(tilex*tileWidth + anchor.x*tileWidth, heightOffset, tiley*tileWidth + anchor.y*tileWidth);
    }
}