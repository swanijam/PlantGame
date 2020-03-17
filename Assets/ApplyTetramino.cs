using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyTetramino : MonoBehaviour
{
    public TileStateManager state;
    public TileTargeting targeting;

    public Tetramino d_activeTetramino;
    float d_currentRotation = 0f;

    public void ApplyCurrentTetramino(int x, int y, float rotation = 0f) {
        for (int i = 0; i < d_activeTetramino.tiles.Length; i++) {
            Vector2Int offs = RotateOffset(d_activeTetramino.tiles[i].offset, rotation);
            state.AddSunlight(x+offs.x, y+offs.y, d_activeTetramino.tiles[i].sunEffect);
            state.AddWater(x+offs.x, y+offs.y, d_activeTetramino.tiles[i].waterEffect);
        }
    }
    private Vector2Int RotateOffset(Vector2Int offset, float rotation) {
        float normalizedRotation = rotation % 360f;
        if (normalizedRotation < 0f) normalizedRotation += 360f;
        if (normalizedRotation < 90f) return new Vector2Int(-offset.y, offset.x);
        if (normalizedRotation < 180f) return new Vector2Int(-offset.x, -offset.y);
        if (normalizedRotation < 270f) return new Vector2Int(offset.y, -offset.x);
        // if (normalizedRotation < 360f) return offset;
        else return offset; // same thing as above comment, but satisfies "all code paths return a value"
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) {
            ApplyCurrentTetramino(targeting.currentTile.x, targeting.currentTile.y, d_currentRotation);
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            d_currentRotation += 90f;
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