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

    // public int numLightnings = 2;
    public bool noNoneDays = true;
    public void Initialize(int numDays=7, int numLightnings = 0) {
        weatherQueue = new List<ForecastType>();
        for (int i = 0; i < numDays-numLightnings; i++) {
            int selection = Random.Range(0, 4);
        // we would rather insert lignthing manually
        while(((ForecastType)selection).Equals(ForecastType.Lightning)) {
            selection = Random.Range(0, 4);
        }
        if (noNoneDays) {
            while(((ForecastType)selection).Equals(ForecastType.None) || ((ForecastType)selection).Equals(ForecastType.Lightning)) {
                selection = Random.Range(0, 4);
            }
        }
        weatherQueue.Add((ForecastType)selection);
        }
        for (int i = 0; i < numLightnings; i++) {
            weatherQueue.Insert(Random.Range(0, weatherQueue.Count), ForecastType.Lightning);
        }
        // Debug.Log("Initialized Weather Queue");
    }
    public void AdvanceWeather() {
        weatherQueue.RemoveAt(0);
    }
}
