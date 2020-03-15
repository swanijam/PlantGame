using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilerRenderer : MonoBehaviour
{
    public TileStateManager state;
    public Camera mainCamera;
    public Transform origin;
    public float tileWidth;
    public float heightOffset;
    public Vector2 boxdimensions;
    // Update is called once per frame
    private void Awake()
    {
    }
    void OnGUI() {
        RenderTilesWithGUI();
    }
    private void RenderTilesWithGUI() {
        Vector3 tileOffset = new Vector3(tileWidth, 0f, tileWidth);
        // buffers
        Rect box;string statusString;Vector3 labelPositionW;Vector2 labelPositionS;
        // draw status box at center of each tile on screen
        for (int x = 0; x < state.dimensions.x; x++ ) {
            for (int y = 0; y < state.dimensions.y; y++ ) {
               labelPositionW = origin.position + Vector3.Scale(tileOffset, new Vector3(x, 0, y)) + heightOffset * Vector3.up + new Vector3(tileWidth/2f, 0f, tileWidth/2f);
               labelPositionS = mainCamera.WorldToScreenPoint(labelPositionW);
               labelPositionS.y = Screen.height - labelPositionS.y; // flip the y coordinate to convert from camera screen space to GUI space.
               box = new Rect(labelPositionS.x, labelPositionS.y, boxdimensions.x, boxdimensions.y);
               Rect box2 = box;
               box2.x -= 3; box2.y -= 3;
               box2.height = 6; box2.width = 6;
               statusString = "sun:"+state.tiles[x, y].sunlightLevel +"\nwtr:"+state.tiles[x, y].waterLevel; 
               GUI.Box(box, statusString);
               GUI.DrawTexture(box2, Texture2D.whiteTexture, ScaleMode.StretchToFill, false, 1f, Color.yellow, 0f, 0f);
            }
        }
    }
}
