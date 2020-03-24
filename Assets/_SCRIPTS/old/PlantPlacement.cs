using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPlacement : MonoBehaviour
{
    public GameObject plant;
    public TileTargeting targeting;
    public TileVisualRenderer visualz;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) {
            Vector2Int targetCell = targeting.currentTile;
            Vector3 pos = targeting.GetWorldPositionFromTileCoordinate(targetCell.x, targetCell.y);
            // parent eh new plant to the rendertilegroup!
            GameObject go = Instantiate(plant, pos, Quaternion.identity);
            visualz.tiles[targetCell.x, targetCell.y].AddVisualizer(go.GetComponent<Visualizer>());
        }
    }
}
