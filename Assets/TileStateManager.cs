using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileStateManager : MonoBehaviour
{
    public Vector2Int dimensions;
    public GameTile[,] tiles;
    // Start is called before the first frame update
    void Awake()
    {
        Initialize();   
    }
    void Initialize() {
        tiles = new GameTile[dimensions.x, dimensions.y];
        for (int x = 0; x < dimensions.x; x++ ) {
            for (int y = 0; y < dimensions.y; y++ ) {
                tiles[x,y] = new GameTile();
            }
        }
    }

    public void AddSunlight(int x, int y, int amt) {
        tiles[x,y].sunlightLevel += amt;
    }
    public void AddWater(int x, int y, int amt) {
        tiles[x,y].waterLevel += amt;
    }
}

[System.Serializable]
public class GameTile {
    public string name = "tile";
    public int sunlightLevel;
    public int waterLevel;
    public GameTile(string name ="", int sunlight =1, int water =1) {
        this.name = name;
        this.sunlightLevel = sunlight;
        this.waterLevel = water;
    }
}
