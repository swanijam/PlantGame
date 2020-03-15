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
        // this gets initialized by the GameManager now
        // Initialize();   
    }
    public void Initialize() {
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
    public int _sunlightLevel;
    public int sunlightLevel {
        get {return _sunlightLevel;}
        set {
            if (value != _sunlightLevel) {
                EmitStateChangeEvent(value-_sunlightLevel, 0);
            }
            _sunlightLevel = value;
        }
    }
    public int _waterLevel;
    public int waterLevel {
        get {return _waterLevel;}
        set {
            if (value != _waterLevel) {
                EmitStateChangeEvent(0, value-_waterLevel);
            }
            _waterLevel = value;
        }
    }
    public GameTile(string name ="", int sunlight =1, int water =1) {
        this.name = name;
        this.sunlightLevel = sunlight;
        this.waterLevel = water;
    }

    // emit state change events using event "onStateChanged"
    // by using a subscriber pattern here, we can have the TileStateManager continue to not care what's rendering it.
    // a PlantRenderer, for example, should initialize by doing [TileStateManager].tiles[x,y].onStateChanged += PlantStateChange;
    public delegate void StateChangeEvent(int sunChange, int waterChange);
    public event StateChangeEvent onStateChanged;
    private void EmitStateChangeEvent(int sunChange, int waterChange) {
        if (onStateChanged != null) onStateChanged(sunChange, waterChange);
    }
}
