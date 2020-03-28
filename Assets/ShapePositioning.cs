using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapePositioning : MonoBehaviour
{
    public ForecastShape currentShape;
    public float rotation;
    public GameObject previewTilePrefab;
    public float tileSpawnHeight = 6f;
    public float tileWidth = 1f;
    public Vector2Int currentOriginTile;

    GameObject[] previewTiles;
    public void BuildTileArray() {
        previewTiles = new GameObject[currentShape.tiles.Count];
        // setup the tiles
        for (int i = 0; i < currentShape.tiles.Count; i++) {
            // Debug.Log(currentShape.tiles[i].offset);
            GameObject newgo = Instantiate(previewTilePrefab, transform);
            previewTiles[i] = newgo;
            newgo.SetActive(true);
            Vector3 pos = PGTileTargeting.instance.GetWorldPositionFromTileCoordinate(currentOriginTile.x, currentOriginTile.y);
            pos.y = tileSpawnHeight;
            previewTiles[i].transform.position = pos + new Vector3(currentShape.tiles[i].offset.x, 0f, currentShape.tiles[i].offset.y) * tileWidth;
        }        
    }
    public void UpdateTileArray(Vector2Int newOrigin, float newRotation) {
        currentOriginTile = newOrigin;
        for (int i = 0; i < currentShape.tiles.Count; i++) {
            Vector3 pos = PGTileTargeting.instance.GetWorldPositionFromTileCoordinate(currentOriginTile.x, currentOriginTile.y);
            pos.y = tileSpawnHeight;
            Vector2Int offset = RotateOffset(currentShape.tiles[i].offset, newRotation);
            previewTiles[i].transform.position = pos + new Vector3(offset.x, 0f, offset.y) * tileWidth;
        }        
    }
    private Vector2Int RotateOffset(Vector2Int offset, float rotation) {
        float normalizedRotation = rotation % 360f;
        // Debug.Log(normalizedRotation);
        if (normalizedRotation < 0f) normalizedRotation += 360f;
        if (normalizedRotation < 90f) return new Vector2Int(-offset.y, offset.x);
        if (normalizedRotation < 180f) return new Vector2Int(-offset.x, -offset.y);
        if (normalizedRotation < 270f) return new Vector2Int(offset.y, -offset.x);
        // if (normalizedRotation < 360f) return offset;
        else return offset; // same thing as above comment, but satisfies "all code paths return a value"
    }

    public bool buildNow = false;
    private void Update()
    {
        if (buildNow) {
            buildNow = false;
            BuildTileArray();
        }
    }

}
