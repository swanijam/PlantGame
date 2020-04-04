using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnQueue : MonoBehaviour
{
     public static TurnQueue instance;
     public Transform TurnActionsGroup;
     public GameObject plantTurnPrefab;
     public GameObject shapeTurnPrefab;
    private void Awake()
    {
        if (instance != null) {
            GameObject.DestroyImmediate(instance.gameObject);
            Debug.LogError("additional weatherqueue found. deleting the older one");
        }
        instance = this;
        // Debug.Log("defined instance");
    }
    public List<TurnAction> turnQueue;
    public static TurnAction currentWeather {
        get {
            return instance.turnQueue[0];
        }
    }

    public int numPlantsAtStart = 5;
    public void Initialize() {
        GameObject go;
        for ( int n = 0; n < numPlantsAtStart; n++) {
            go = Instantiate(plantTurnPrefab, TurnActionsGroup);
            turnQueue.Add(go.GetComponent<TurnAction>());
        }
        for ( int n = 0; n < WeatherQueue.numDays; n++) {
            go = Instantiate(shapeTurnPrefab, TurnActionsGroup);
            turnQueue.Add(go.GetComponent<TurnAction>());
        }
        BeginTurnTakingLoop();
    }

    public void BeginTurnTakingLoop() {
        StartCoroutine(TurnTakingLoop());
    }
    IEnumerator TurnTakingLoop() {
        for (int i = 0; i < turnQueue.Count; i++) {
            turnQueue[i].gameObject.SetActive(true);
            turnQueue[i].PrepareAction();
            while (turnQueue[i].done == false) {
                yield return null;
            }
            turnQueue[i].gameObject.SetActive(false);
        }
        Debug.Log("COMPLETED LEVEL");
    }
}
