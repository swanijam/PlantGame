using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public ForecastType currentlyWanting;
    public Transform plantT;
    int HEALTH = 1;
    private void OnEnable()
    {
        BeginNewTurn();
    }

    public void ReceiveWeather(ForecastType type) {
        receivedThisTurn = type;
    }
    
    public void AdvanceTurn() {
        ResolveTurn();
        BeginNewTurn();
    }
    bool noNoneDays = true;
    bool changeNeedEveryTurn = false;
    bool needMet = true;
    public void BeginNewTurn() {
        receivedThisTurn = ForecastType.None;
        if (!changeNeedEveryTurn && !needMet && !currentlyWanting.Equals(ForecastType.None)) return;
        int selection = Random.Range(0, 3);
        if (noNoneDays) {
            while(((ForecastType)selection).Equals(ForecastType.None)) {
                selection = Random.Range(0, 3);
            }
        }
        currentlyWanting = (ForecastType)selection;
        AnnounceWant();
    }

    ForecastType receivedThisTurn = ForecastType.None;
    public void ResolveTurn() {
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
        // GameObject.DestroyImmediate(this.gameObject);
    }

    public void Grow() {
        needMet = true;
        plantT.localScale = new Vector3(plantT.localScale.x, plantT.localScale.y*1.5f, plantT.localScale.z);
        if (!changeNeedEveryTurn) currentlyWanting = ForecastType.None; 
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
