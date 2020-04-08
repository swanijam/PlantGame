using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherQueuePanel : MonoBehaviour
{
    public GameObject rainWeatherImgPrefab, sunWeatherImgPrefab, noWeatherImgPrefab, lightningWeatherImgPrefab;
    public RectTransform weatherGroup;

    List<GameObject> weatherItems;
    public void Initialize() {
        weatherItems = new List<GameObject>();
        ForecastType[] weathers = WeatherQueue.GetWeatherArray();
        GameObject newGo;
        for (int i = 0; i < weathers.Length; i++) {
            switch (weathers[i]) {
                case ForecastType.Water:
                    newGo = Instantiate(rainWeatherImgPrefab, weatherGroup.position, Quaternion.identity, weatherGroup);
                break;
                case ForecastType.Sun:
                    newGo = Instantiate(sunWeatherImgPrefab, weatherGroup.position, Quaternion.identity, weatherGroup);
                break;
                case ForecastType.Lightning:
                    newGo = Instantiate(lightningWeatherImgPrefab, weatherGroup.position, Quaternion.identity, weatherGroup);
                break;
                default:
                    newGo = Instantiate(noWeatherImgPrefab, weatherGroup.position, Quaternion.identity, weatherGroup);
                break;
            }
            newGo.SetActive(true);
            weatherItems.Add(newGo);
        }
        // Debug.Log("Initialized Weather Queue Panel");
    }

    public void AdvanceWeatherQueuePanel() {
        StartCoroutine(_RemoveElement());
    }
    public AnimationCurve ScaleDownCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    public float ScaleDownTime = 1f;
    private IEnumerator _RemoveElement() {
        RectTransform rt = weatherItems[0].GetComponent<RectTransform>();
        Vector2 startSize = rt.sizeDelta;
        float currTime = 0f;
        float lerpVal;
        WaitForEndOfFrame wfeof = new WaitForEndOfFrame();
        while (currTime < ScaleDownTime) {
            currTime += Time.deltaTime;
            lerpVal = 1f-ScaleDownCurve.Evaluate(Mathf.InverseLerp(0f, ScaleDownTime, currTime));
            rt.sizeDelta = startSize * lerpVal;
            yield return wfeof;
        }
        GameObject.Destroy(weatherItems[0]);
        weatherItems.RemoveAt(0);
    }
}
