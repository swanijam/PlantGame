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

<<<<<<< HEAD
=======
[System.Serializable]
public class TileRenderGroup
{
    public List<RenderTile> renderTiles;   
    public TileRenderGroup() {
        renderTiles = new List<RenderTile>();
    }
    // receives stateChangeEvents from tiles in the tileStateManager and passes them to renderers
    public void OnTileStateChanged(int sunLevel, int waterLevel) {
        for(int i = 0; i < renderTiles.Count; i++) {
            renderTiles[i].OnStateChanged(sunLevel, waterLevel);
        }
    }

    public void InitializeState(int sunLevel, int waterLevel) {
        for(int i = 0; i < renderTiles.Count; i++) {
            renderTiles[i].InitializeState(sunLevel, waterLevel);
        }
    }

    public void AddRenderTile(RenderTile renderTile) {
        renderTiles.Add(renderTile);
    }
}
>>>>>>> 040fafc35be7f94042b8e06fb990395d6f4e125f
