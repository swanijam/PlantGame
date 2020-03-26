using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ForecastGroup", menuName = "ForecastGroup", order = 1)]
public class ForecastShapeGroup : ScriptableObject
{
    public ForecastShape[] shapes;
    public ForecastShapeGroup() {
        shapes = new ForecastShape[1];
    }
}
