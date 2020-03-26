using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGameManager : MonoBehaviour
{
    public WeatherQueue weatherQueue;
    public WeatherQueuePanel weatherQueuePanel;
    public PGTileStateManager tileStateManager;

    private void Start()
    {
        weatherQueue.Initialize();
        weatherQueuePanel.Initialize();
        tileStateManager.Initialize();
    }
}
