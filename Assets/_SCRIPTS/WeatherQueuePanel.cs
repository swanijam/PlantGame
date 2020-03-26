﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherQueuePanel : MonoBehaviour
{
    public GameObject rainWeatherImgPrefab, sunWeatherImgPrefab, noWeatherImgPrefab;
    public RectTransform weatherGroup;

    public void Initialize() {
        Weather[] weathers = WeatherQueue.GetWeatherArray();
        GameObject newGo;
        for (int i = 0; i < weathers.Length; i++) {
            switch (weathers[i]) {
                case Weather.Rain:
                    newGo = Instantiate(rainWeatherImgPrefab, weatherGroup.position, Quaternion.identity, weatherGroup);
                break;
                case Weather.Sun:
                    newGo = Instantiate(sunWeatherImgPrefab, weatherGroup.position, Quaternion.identity, weatherGroup);
                break;
                default:
                    newGo = Instantiate(noWeatherImgPrefab, weatherGroup.position, Quaternion.identity, weatherGroup);
                break;
            }
            newGo.SetActive(true);
        }
        // Debug.Log("Initialized Weather Queue Panel");
    }
}