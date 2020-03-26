using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGameManager : MonoBehaviour
{
    public WeatherQueue weatherQueue;
    public WeatherQueuePanel weatherQueuePanel;
    public PGTileStateManager tileStateManager;
    public ShapeQueue shapeQueue;
    public ShapeQueuePanel shapeQueuePanel;

    private void Start()
    {
        weatherQueue.Initialize();
        weatherQueuePanel.Initialize();
        tileStateManager.Initialize();
        shapeQueue.Initialize();
        shapeQueuePanel.Initialize();
    }
}
