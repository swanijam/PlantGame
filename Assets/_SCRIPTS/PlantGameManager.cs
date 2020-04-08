using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGameManager : MonoBehaviour
{
    public static PlantGameManager instance;
    private void Awake()
    {
        if (instance != null) {
            Destroy(instance);
            Debug.Log("additional plantgamemanager found. Destroying the old one.");
        }
        instance = this;
    }

    public WeatherQueue weatherQueue;
    public WeatherQueuePanel weatherQueuePanel;
    public PGTileStateManager tileStateManager;
    public ShapeQueue shapeQueue;
    public ShapeQueuePanel shapeQueuePanel;
    public ShapePositioning shapePositioning;
    public TurnQueue turnQueue;

    private void Start()
    {
        weatherQueue.Initialize();
        weatherQueuePanel.Initialize();
        tileStateManager.Initialize();
        shapeQueue.Initialize(WeatherQueue.numDays);
        shapeQueuePanel.Initialize();
        turnQueue.Initialize();
    }

    public void ClearForecastShapeSelection() {
        shapePositioning.ClearPreviewTiles();
        shapePositioning._currentShape = null;
        shapeQueuePanel.CancelPreparedSelections();
        shapeQueuePanel.RemoveElement(shapeQueue._selectedShape);
        shapeQueue.RemoveSelectedShape();
    }

    public void AdvanceWeather() {
        weatherQueue.AdvanceWeather();
        weatherQueuePanel.AdvanceWeatherQueuePanel();
    }

    public UnityEngine.UI.Text endText;
    public void CompleteGame() {
        endText.gameObject.SetActive(true);
        endText.text = "U GOT " + tileStateManager.DoAHarvest() + " PLANTS TO HARVEST";
    }
}
