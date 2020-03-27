using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PGTileStateManager : MonoBehaviour
{ 
    public static PGTileStateManager instance;
  
    public Vector2Int dimensions;
    public List<List<PGTile>> tiles;

    public Transform origin;
    public float tileWidth = 1;
    public float heightOffset;
    public GameObject renderGroupPrefab;
    public GameObject SoilRenderPrefab;

    void Awake()
    {
        if (instance != null) {
            GameObject.DestroyImmediate(instance);
            Debug.LogError("duplicate instance of singleton found. deleting the old one.");
        }
        instance = this;
        // this gets initialized by the GameManager now
        // Initialize();
    }
    public void ClearState() {
        for (int x = 0; x < dimensions.x; x++ ) {
            tiles[x].Clear();
        }
        tiles.Clear();
    }
    public void Initialize() {
        if (animatedInit) {
            InitializeAnimated();
        } else {
            tiles = new List<List<PGTile>>(dimensions.x);
            for (int x = 0; x < dimensions.x; x++ ) {
                tiles[x] = new List<PGTile>();
                for (int y = 0; y < dimensions.y; y++ ) {
                    tiles[x][y] = new PGTile();
                    GameObject go = new GameObject(x + "," + y);
                    go.transform.SetParent(transform);
                    tiles[x][y].soilRenderer = Instantiate(SoilRenderPrefab, GetPosition(x,y,new Vector2(.5f, .5f)), Quaternion.identity, go.transform);
                    tiles[x][y].currentPlant = null;
                }
            }
            Debug.Log("Initialized Board State");
        }
    }
    public void InitializeAnimated() {
        StopAllCoroutines();
        StartCoroutine(AnimatedInitialize());
    }
    public bool animatedInit = true;
    public float animatedInitializeInterval = .05f;
    private IEnumerator AnimatedInitialize() {
        tiles = new List<List<PGTile>>();
        for (int x = 0; x < dimensions.x; x++ ) {
            tiles.Add(new List<PGTile>());
            for (int y = 0; y < dimensions.y; y++ ) {
                yield return new WaitForSeconds(animatedInitializeInterval);
                tiles[x].Add(new PGTile());
                GameObject go = new GameObject(x + "," + y);
                go.transform.SetParent(transform);
                tiles[x][y].soilRenderer = Instantiate(SoilRenderPrefab, GetPosition(x,y,new Vector2(.5f, .5f)), Quaternion.identity, go.transform);
                tiles[x][y].currentPlant = null;
            }
        }
        // Debug.Log("Initialized Board State");
    }
    public bool ValidCoordinate(int x, int y) {
        if (x < 0 || x >= dimensions.x)
            return false;
        if (y < 0 || y >= dimensions.y)
            return false;
        return true;
    }
    // public void AddSunlight(int x, int y, int amt) {
    //     if (!ValidCoordinate(x,y)) return;
    // }
    // public void AddWater(int x, int y, int amt) {
    //     if (!ValidCoordinate(x,y)) return;
    // }
    public void AddWeather(int x, int y, ForecastType type, int amt) {
        if (!ValidCoordinate(x,y)) return;
        tiles[x][y].currentPlant.ReceiveWeather(type);
    }
    public bool HasPlant(int x, int y) {
        if (!ValidCoordinate(x,y)) return true;
        return tiles[x][y].currentPlant != null;
    }
    // given a grid coordinate, returns position of tile + anchor*tileWidth.
    // so GetPosition(2,3, (.5f, .5f)) return the center of the tile in the 3rd row and 4th column.
    public Vector3 GetPosition(int tilex, int tiley, Vector2 anchor) {
        return origin.position + new Vector3(tilex*tileWidth + anchor.x*tileWidth, heightOffset, tiley*tileWidth + anchor.y*tileWidth);
    }
}

[System.Serializable]
public class PGTile {
    public GameObject soilRenderer;
    public Plant currentPlant;
    // emit state change events using event "onStateChanged"
    // by using a subscriber pattern here, we can have the TileStateManager continue to not care what's rendering it.
    // a PlantRenderer, for example, should initialize by doing [TileStateManager].tiles[x,y].onStateChanged += PlantStateChange;
    public delegate void StateChangeEvent(int newSun, int newWater);
    public event StateChangeEvent onStateChanged;
    private void EmitStateChangeEvent(int newSun, int newWater) {
        if (onStateChanged != null) onStateChanged(newSun, newWater);
    }


}
