using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public ForecastType currentlyWanting;
    public Transform plantT;
    [HideInInspector]
    public Vector2Int tileCoordinate;
    int HEALTH = 1;
    int _growth = 0;
    public int growth {
        get {return _growth;}
        set {
            _growth=value;
            if (_growth >= 3) {
                harvestable = true;
                harvestableVisual.SetActive(true);
            }
        }
    }
    public bool harvestable = false;
    public GameObject harvestableVisual;
    private void OnEnable()
    {
        BeginNewTurn();
    }

    public void ReceiveWeather(ForecastType type) {
        receivedThisTurn = type;
        Debug.Log(currentlyWanting +";"+ receivedThisTurn);
    }
    
    public void AdvanceTurn() {
        ResolveTurn();
        BeginNewTurn();
    }
    bool noNoneDays = true;
    bool changeNeedEveryTurn = false;
    bool initialized = false;
    bool needMet = false;
    bool alwaysAlternateAfterNeedMet = true;
    public void BeginNewTurn() {
        Debug.Log("start new turn: " + currentlyWanting);
        receivedThisTurn = ForecastType.None;
        
        if (initialized && !changeNeedEveryTurn && !needMet && !currentlyWanting.Equals(ForecastType.None)) {
            Debug.Log("cancelling because need wasn't met");
            return;
        }
            // override the randomness if we choose to alternate instead
        if (initialized && alwaysAlternateAfterNeedMet && needMet) {
            Debug.Log("Alternating " + currentlyWanting);
            if (currentlyWanting.Equals(ForecastType.Water)) currentlyWanting = ForecastType.Sun;
            else if (currentlyWanting.Equals(ForecastType.Sun)) currentlyWanting = ForecastType.Water;
            Debug.Log("Done Alternating " + currentlyWanting);
        } else {
            Debug.Log("randomizing new want");
            int selection = Random.Range(0, 4);
            // plants don't want lightning
            while(((ForecastType)selection).Equals(ForecastType.Lightning)) {
                selection = Random.Range(0, 4);
            }
            if (noNoneDays) {
                while(((ForecastType)selection).Equals(ForecastType.None) || ((ForecastType)selection).Equals(ForecastType.Lightning)) {
                    selection = Random.Range(0, 4);
                }
            }
            currentlyWanting = (ForecastType)selection;
            Debug.Log("random new want: " + currentlyWanting);
        }
        // Debug.Log(currentlyWanting);
        initialized = true;
        AnnounceWant();
    }

    ForecastType receivedThisTurn = ForecastType.None;
    public void ResolveTurn() {
        // this implicitly includes lightning because plants never want lightning
        if (!receivedThisTurn.Equals(currentlyWanting) && !receivedThisTurn.Equals(ForecastType.None)) {
            HEALTH--;
        }
        if (HEALTH <= 0) {
            Die();
        } else if(receivedThisTurn.Equals(currentlyWanting)) {
            Grow();   
        } else {
            // this means it didn't die and it didn't get what it wanted
            needMet = false;
        }
    }

    public void Die() {
        gameObject.SetActive(false);
        PGTileStateManager.instance.tiles[tileCoordinate.x][tileCoordinate.y].currentPlant = null;
        // GameObject.DestroyImmediate(this.gameObject);
    }

    public void Grow() {
        growth++;
        needMet = true;
        if (growth > 3) return;
        plantT.localScale = new Vector3(plantT.localScale.x, plantT.localScale.y*1.5f, plantT.localScale.z);
        // if (!changeNeedEveryTurn) currentlyWanting = ForecastType.None; 
    }

    public GameObject wantUIwtr, wantUIsun;
    public void AnnounceWant() {
        switch(currentlyWanting) {
            case ForecastType.None:
                wantUIwtr.SetActive(false);
                wantUIsun.SetActive(false);
                break;
            case ForecastType.Water:
                wantUIwtr.SetActive(true);
                wantUIsun.SetActive(false);
                break;
            case ForecastType.Sun:
                wantUIwtr.SetActive(false);
                wantUIsun.SetActive(true);
                break;
        }
    }

}
