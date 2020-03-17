using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyTetramino : MonoBehaviour
{
    public TileStateManager state;
    public TileTargeting targeting;
    public Tetramino activeTetramino;

    public void ApplyCurrentTetramino(int x, int y) {
        for (int i = 0; i < activeTetramino.tiles.Length; i++) {
            state.AddSunlight(x+activeTetramino.tiles[i].offset.x, y+activeTetramino.tiles[i].offset.y, activeTetramino.tiles[i].sunEffect);
            state.AddWater(x+activeTetramino.tiles[i].offset.x, y+activeTetramino.tiles[i].offset.y, activeTetramino.tiles[i].waterEffect);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) {
            ApplyCurrentTetramino(targeting.currentTile.x, targeting.currentTile.y);
        }
    }
}

[System.Serializable]
public class Tetramino {
    public TetraminoTile[] tiles;
}
[System.Serializable]
public class TetraminoTile {
    public Vector2Int offset;
    public int sunEffect;
    public int waterEffect;
}