using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//State of each tile
public enum ForecastType {None, Water, Sun};
 
public class ForecastQueue : MonoBehaviour
{
	//Each of the shapes in the queue
    [HideInInspector] public List<ForecastShape> forecastShapes = new List<ForecastShape>();
	
	
	//PaintMode
	[HideInInspector] public ForecastType PaintMode = ForecastType.Water; 



	
}
