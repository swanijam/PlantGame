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
    public GameObject previewTileWtr;
    public GameObject previewTileSun;
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
        }
    }
    
    public static float PLACE_TILE_TIME = .75f; 
    public static float ROTATE_TIME = .4f;
    public static float DRAG_DISTANCE = 10f;
    public Vector2Int currentOriginTile;

    public delegate void PlaceShapeEvenet();
    public event PlaceShapeEvenet onPlaceShape;

    GameObject[] previewTiles;
    public void ClearPreviewTiles() {
        if (previewTiles != null) {
            for (int i = 0; i < previewTiles.Length; i++) {
                GameObject g = previewTiles[i];
                previewTiles[i] = null;
                GameObject.Destroy(g);
            }
        }
    }
    public void BuildTileArray() {
        ClearPreviewTiles();
        previewTiles = new GameObject[currentShape.tiles.Count];
        // setup the tiles
        // Debug.Log("===========================");
        for (int i = 0; i < currentShape.tiles.Count; i++) {
            // Debug.Log(currentShape.tiles[i].offset);
            // Debug.Log(currentShape.tiles[i].offset);
            GameObject newgo;
            switch(WeatherQueue.currentWeather) {
                case ForecastType.Water:
                    newgo = Instantiate(previewTileWtr, transform);
                    break;
                case ForecastType.Sun:
                    newgo = Instantiate(previewTileSun, transform);
                    break;
                default:
                    newgo = Instantiate(previewTileWtr, transform);
                    break;
            }
            previewTiles[i] = newgo;
            newgo.SetActive(true);
            Vector3 pos = PGTileTargeting.instance.GetWorldPositionFromTileCoordinate(currentOriginTile.x, currentOriginTile.y);
            pos.y = tileSpawnHeight;
            previewTiles[i].transform.position = pos + new Vector3(currentShape.tiles[i].offset.x, 0f, currentShape.tiles[i].offset.y) * tileWidth;
        }        
    }
    IEnumerator BuildTileArraySlow() {
        // ClearPreviewTiles();
        // previewTiles = new GameObject[currentShape.tiles.Count];
        // // setup the tiles
        // Debug.Log("===========================");
        // for (int i = 0; i < currentShape.tiles.Count; i++) {
        //     Debug.Log(currentShape.tiles[i].offset);
        //     // Debug.Log(currentShape.tiles[i].offset);
        //     GameObject newgo;
        //     switch(WeatherQueue.currentWeather) {
        //         case ForecastType.Water:
        //             newgo = Instantiate(previewTileWtr, transform);
        //             break;
        //         case ForecastType.Sun:
        //             newgo = Instantiate(previewTileSun, transform);
        //             break;
        //         default:
        //             newgo = Instantiate(previewTileWtr, transform);
        //             break;
        //     }
        //     previewTiles[i] = newgo;
        //     newgo.SetActive(true);
        //     Vector3 pos = PGTileTargeting.instance.GetWorldPositionFromTileCoordinate(currentOriginTile.x, currentOriginTile.y);
        //     pos.y = tileSpawnHeight;
        //     previewTiles[i].transform.position = pos + new Vector3(currentShape.tiles[i].offset.x, 0f, currentShape.tiles[i].offset.y) * tileWidth;
        //     yield return new WaitForSeconds(2f);
        // }  
        GameObject newgo = Instantiate(previewTileWtr, transform);
        newgo.SetActive(true);
        while(true) {
            newgo.transform.position = newgo.transform.position + new Vector3(0f, 0f, 1f) * tileWidth;
            yield return new WaitForSeconds(2f);
        }
    }
    public void UpdateTileArray() {
        for (int i = 0; i < currentShape.tiles.Count; i++) {
            Vector3 pos = PGTileTargeting.instance.GetWorldPositionFromTileCoordinate(currentOriginTile.x, currentOriginTile.y);
            pos.y = tileSpawnHeight;
            Vector2Int offset = RotateOffset(currentShape.tiles[i].offset, rotation);
            // Debug.Log(rotation +", "+ offset +", "+ currentShape.tiles[i].offset);
            previewTiles[i].transform.position = pos + new Vector3(offset.x, 0f, offset.y) * tileWidth;
        }        
    }
    public void UpdateOrigin(Vector2Int newOriginTile) {
        currentOriginTile = newOriginTile;
        UpdateTileArray();
    }
    public void UpdateRotation(float newRotation) {
        rotation = newRotation;
        UpdateTileArray();
    }
    public Vector2Int RotateOffset(Vector2Int offset, float rotation) {
        float normalizedRotation = rotation % 360f;
        if (normalizedRotation < 0f) normalizedRotation += 360f;
        if (normalizedRotation == 0f) return offset;
        if (normalizedRotation == 90f) return new Vector2Int(-offset.y, offset.x);
        if (normalizedRotation == 180f) return new Vector2Int(-offset.x, -offset.y);
        if (normalizedRotation == 270f) return new Vector2Int(offset.y, -offset.x);
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
        // update shape preview position
        if (currentlyDragging) {
                Vector2Int a, b;
                // a = PGTileTargeting.instance.GetTileCoordinateFromScreenPosition(dragOrigin);
                b = PGTileTargeting.instance.GetTileCoordinateFromScreenPosition(Input.mousePosition);
                UpdateOrigin(b);
        }
        
        // end drag if we let go
        if (Input.GetMouseButtonUp(0)) {
            if (currentlyDragging) EndDragging();
        }
    }

    bool currentlyDragging = false;
    public void BeginDraggingPiece(ForecastShape shape, float _rotation = 0f) {
        rotation = _rotation;
        dragOrigin = Input.mousePosition;
        currentShape = shape;
        currentlyDragging = true;
        // StartCoroutine(BuildTileArraySlow());
        BuildTileArray();
        UpdateTileArray();
    }
    public void EndDragging() {
        applyForecastShape.ApplyCurrentTetramino(currentShape);
        if (onPlaceShape != null)  onPlaceShape();
        currentShape = null;
        currentlyDragging = false;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ShapePositioning))]
public class ShapePositioningEditor : Editor{
    SerializedProperty currentShape;
    SerializedProperty previewTileWtr;
    SerializedProperty previewTileSun;
    SerializedProperty tileSpawnHeight;
    SerializedProperty tileWidth;
    SerializedProperty currentOriginTile;
    SerializedProperty applyForecastShape;
    // // [HideInInspector]
    private void OnEnable()
    {
        currentShape = serializedObject.FindProperty("_currentShape");
        previewTileWtr = serializedObject.FindProperty("previewTileWtr");
        previewTileSun = serializedObject.FindProperty("previewTileSun");
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
        EditorGUILayout.PropertyField(previewTileWtr);
        EditorGUILayout.PropertyField(previewTileSun);
        EditorGUILayout.PropertyField(tileSpawnHeight);
        EditorGUILayout.PropertyField(tileWidth);
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
