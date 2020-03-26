using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weather{Sun, Rain, None}
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
    public Queue<Weather> weatherQueue;
    public static Weather currentWeather {
        get {
            return instance.weatherQueue.Peek();
        }
    }

    public static Weather GetWeather(int day) {
        return instance.weatherQueue.ToArray()[day-1];
    }

    public static Weather[] GetWeatherArray() {
        return instance.weatherQueue.ToArray();
    }

    public bool noNoneDays = true;
    public void Initialize(int numDays=10) {
        weatherQueue = new Queue<Weather>();
        for (int i = 0; i < numDays; i++) {
            int selection = Random.Range(0, 3);
            if (noNoneDays) {
                while(((Weather)selection).Equals(Weather.None)) {
                    selection = Random.Range(0, 3);
                }
            }
            weatherQueue.Enqueue((Weather)selection);
        }
        // Debug.Log("Initialized Weather Queue");
    }

    public void AdvanceQueue() {
        weatherQueue.Dequeue();
    }
}
