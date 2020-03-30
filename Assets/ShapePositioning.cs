using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class ShapePositioning : MonoBehaviour
{

    public static ShapePositioning instance;
    private void Awake()
    {
        if (instance != null) {
            GameObject.Destroy(instance);
            Debug.LogError("additional instance of ShapePositioning found. Destroying the old one.");
        }
        instance = this;
    }
    [HideInInspector]
    public float rotation;
    public GameObject previewTilePrefab;
    public float tileSpawnHeight = 6f;
    public float tileWidth = 1f;
    public PGApplyTetramino applyForecastShape;
    
    public ForecastShape _currentShape;
    public static ForecastShape currentShape {
        get {
            return instance._currentShape;
        }
        set {
            instance._currentShape = value;
            instance.applyForecastShape.currentShape = value;
        }
    }
    
    public static float PLACE_TILE_TIME = 2f; 
    public static float ROTATE_TIME = 1f;
    public static float DRAG_DISTANCE = 10f;
    public Vector2Int currentOriginTile;


    GameObject[] previewTiles;
    public void BuildTileArray() {
        if (previewTiles != null) {
            for (int i = 0; i < previewTiles.Length; i++) {
                GameObject g = previewTiles[i];
                previewTiles[i] = null;
                GameObject.Destroy(g);
            }
        }
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
    public void UpdateTileArray() {
        for (int i = 0; i < currentShape.tiles.Count; i++) {
            Vector3 pos = PGTileTargeting.instance.GetWorldPositionFromTileCoordinate(currentOriginTile.x, currentOriginTile.y);
            pos.y = tileSpawnHeight;
            Vector2Int offset = RotateOffset(currentShape.tiles[i].offset, rotation);
            previewTiles[i].transform.position = pos + new Vector3(offset.x, 0f, offset.y) * tileWidth;
        }        
    }
    public void UpdateOrigin(Vector2Int delta) {
        currentOriginTile = currentOriginTile + delta;
        UpdateTileArray();
    }
    public void UpdateRotation(float delta) {
        rotation += delta;
        UpdateTileArray();
    }
    public Vector2Int RotateOffset(Vector2Int offset, float rotation) {
        float normalizedRotation = rotation % 360f;
        // Debug.Log(normalizedRotation);
        if (normalizedRotation < 0f) normalizedRotation += 360f;
        if (normalizedRotation < 90f) return new Vector2Int(-offset.y, offset.x);
        if (normalizedRotation < 180f) return new Vector2Int(-offset.x, -offset.y);
        if (normalizedRotation < 270f) return new Vector2Int(offset.y, -offset.x);
        // if (normalizedRotation < 360f) return offset;
        else return offset; // same thing as above comment, but satisfies "all code paths return a value"
    }

    float pressTime = 0f;
    bool blockUntilMouseUp = false;
    Vector3 dragOrigin;
    private float Delta() {
        float delta = (Input.mousePosition - dragOrigin).magnitude;
        return delta;
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            dragOrigin = Input.mousePosition;
        }
     
        if (!blockUntilMouseUp && Input.GetMouseButton(0)) {
            pressTime += Time.deltaTime;
            if (pressTime >= PLACE_TILE_TIME && Delta() < DRAG_DISTANCE) {
                blockUntilMouseUp = true;
                applyForecastShape.ApplyCurrentTetramino();
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            if (Delta() < DRAG_DISTANCE) {
                blockUntilMouseUp = false;
                if (pressTime <= ROTATE_TIME) {
                    UpdateRotation(90f);
                }
            } else {
                Vector2Int a, b;
                a = PGTileTargeting.instance.GetTileCoordinateFromScreenPosition(dragOrigin);
                b = PGTileTargeting.instance.GetTileCoordinateFromScreenPosition(Input.mousePosition);
                UpdateOrigin(b-a);
            }
            pressTime = 0f;
        }
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(ShapePositioning))]
public class ShapePositioningEditor : Editor{
    SerializedProperty currentShape;
    SerializedProperty previewTilePrefab;
    SerializedProperty tileSpawnHeight;
    SerializedProperty tileWidth;
    SerializedProperty currentOriginTile;
    SerializedProperty applyForecastShape;
    // // [HideInInspector]
    private void OnEnable()
    {
        currentShape = serializedObject.FindProperty("_currentShape");
        previewTilePrefab = serializedObject.FindProperty("previewTilePrefab");
        tileSpawnHeight = serializedObject.FindProperty("tileSpawnHeight");
        tileWidth = serializedObject.FindProperty("tileWidth");
        currentOriginTile = serializedObject.FindProperty("currentOriginTile");
        applyForecastShape = serializedObject.FindProperty("applyForecastShape");
    }
    public override void OnInspectorGUI() {
        serializedObject.Update();
        GUI.enabled = false;
        EditorStyles.label.wordWrap = true;
        EditorGUILayout.LabelField("the shape used for ShapePositioning is from the ShapeQueue interface selection.");
        // EditorStyles.label.wordWrap = false;
        EditorGUILayout.PropertyField(currentShape);
        EditorGUILayout.PropertyField(currentOriginTile);
        GUI.enabled = true;
        EditorGUILayout.PropertyField(applyForecastShape);
        EditorGUILayout.PropertyField(previewTilePrefab);
        EditorGUILayout.PropertyField(tileSpawnHeight);
        EditorGUILayout.PropertyField(tileWidth);
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
