﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PGTileTargeting : MonoBehaviour
{
    public static PGTileTargeting instance;
    private void Awake()
    {
        if (instance != null) {
            GameObject.DestroyImmediate(instance);
            Debug.LogError("duplicate instance of singleton found. deleting the old one.");
        }
        instance = this;
    }
    public Texture GUITargetImage;
    Vector3 currentTilePos;
    public Vector2Int currentTile;
    public static Vector3 currentTileCenter {
        get {
            return instance.GetWorldPositionFromTileCoordinate(instance.currentTile.x, instance.currentTile.y);
        }
    }
    public float floorHeight = 0f;
    public Camera mainCam;
    public Transform boardOrigin;
    public float tileWidth = 1f;

    // Update is called once per frame
    void Update()
    {
        Vector3 planePos = GetPlaneIntersection(Input.mousePosition);
        GetNearestTileCenterAndTileCoordinateFromWorldPosition(planePos, out currentTilePos, out currentTile, floorHeight);
    }

    public Vector3 GetNearestTileCenter(Vector3 worldPosition, float worldHeight = 0) {
        Vector3 boardSpace = worldPosition - boardOrigin.position;
        currentTile = new Vector2Int((int)boardSpace.x, (int)boardSpace.z);
        Vector3 snappedCorner = new Vector3(Mathf.Floor(boardSpace.x * tileWidth)/tileWidth, worldHeight, Mathf.Floor(boardSpace.z * tileWidth)/tileWidth);
        snappedCorner.x += tileWidth/2f;
        snappedCorner.z += tileWidth/2f;
        return snappedCorner;
    }
    public Vector2Int GetTileCoordinateFromWorldPosition(Vector3 worldPosition) {
        Vector3 boardSpace = worldPosition - boardOrigin.position;
        return new Vector2Int((int)boardSpace.x, (int)boardSpace.z);
    }
    public void GetNearestTileCenterAndTileCoordinateFromWorldPosition(Vector3 worldPosition, out Vector3 centerPosition, out Vector2Int tileCoordinate, float worldHeight = 0) {
        Vector3 boardSpace = worldPosition - boardOrigin.position;
        tileCoordinate = new Vector2Int((int)boardSpace.x, (int)boardSpace.z);
        Vector3 snappedCorner = new Vector3(Mathf.Floor(boardSpace.x * tileWidth)/tileWidth, worldHeight, Mathf.Floor(boardSpace.z * tileWidth)/tileWidth);
        snappedCorner.x += tileWidth/2f;
        snappedCorner.z += tileWidth/2f;
        centerPosition = snappedCorner;
    }
    public Vector3 GetWorldPositionFromTileCoordinate(int x, int y) {
        Vector3 snappedCorner = new Vector3(Mathf.Floor(x * tileWidth)/tileWidth, floorHeight, Mathf.Floor(y * tileWidth)/tileWidth);
        snappedCorner.x += tileWidth/2f;
        snappedCorner.z += tileWidth/2f;
        return snappedCorner;
    }
    public Vector3 GetWorldPositionFromScreenPosition(Vector3 mousePosition) {
        return GetPlaneIntersection(mousePosition);
    }
    public Vector2Int GetTileCoordinateFromScreenPosition(Vector3 mousePosition) {
        return GetTileCoordinateFromWorldPosition(GetPlaneIntersection(mousePosition));
    }

    private void OnGUI()
    {
        Vector3 labelPositionW = currentTilePos;
        Vector3 labelPositionS = mainCam.WorldToScreenPoint(labelPositionW);
        labelPositionS.y = Screen.height - labelPositionS.y; // flip the y coordinate to convert from camera screen space to GUI space.
        Rect box = new Rect(labelPositionS.x-16, labelPositionS.y-16, 32, 32);
        GUI.DrawTexture(box, GUITargetImage, ScaleMode.StretchToFill, true, 1f, Color.yellow, 0f, 0f);

        Vector3 screenMousePoint = Input.mousePosition;
        screenMousePoint.y = Screen.height - screenMousePoint.y; // flip the y coordinate to convert from camera screen space to GUI space.
        Rect mouseTileLabelRect = new Rect(screenMousePoint.x-50, screenMousePoint.y-20, 50, 20);
        GUI.Box(mouseTileLabelRect, currentTile.ToString());
    }

    private Vector3 GetPlaneIntersection(Vector3 mousePosition)
    {
        Ray ray = mainCam.ScreenPointToRay(mousePosition);
        float delta = ray.origin.y - floorHeight;
        Vector3 dirNorm = ray.direction / ray.direction.y;
        Vector3 intersectionPos = ray.origin - dirNorm * delta;
        return intersectionPos;
    }
}
