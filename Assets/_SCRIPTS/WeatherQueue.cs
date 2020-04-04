using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public enum Weather{Sun, Rain, None}
public class WeatherQueue : MonoBehaviour
{
    public static WeatherQueue instance;
    private void Awake()
    {
        if (instance != null) {
            GameObject.DestroyImmediate(instance.gameObject);
            Debug.LogError("additional weatherqueue found. deleting the older one");
        }
        instance = this;
        // Debug.Log("defined instance");
    }
    public List<ForecastType> weatherQueue;
    public static ForecastType currentWeather {
        get {
            return instance.weatherQueue[0];
        }
    }
    public static int numDays {
        get {
            return instance.weatherQueue.Count;
        }
    }

    public static ForecastType GetWeather(int day) {
        return instance.weatherQueue.ToArray()[day-1];
    }

    public static ForecastType[] GetWeatherArray() {
        return instance.weatherQueue.ToArray();
    }

    public bool noNoneDays = true;
    public void Initialize(int numDays=10) {
        weatherQueue = new List<ForecastType>();
        for (int i = 0; i < numDays; i++) {
            int selection = Random.Range(0, 3);
            if (noNoneDays) {
                while(((ForecastType)selection).Equals(ForecastType.None)) {
                    selection = Random.Range(0, 3);
                }
            }
            weatherQueue.Add((ForecastType)selection);
        }
        // Debug.Log("Initialized Weather Queue");
    }
    public void AdvanceWeather() {
        weatherQueue.RemoveAt(0);
    }
}
