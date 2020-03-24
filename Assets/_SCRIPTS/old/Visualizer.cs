using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///abstract class from which soilrenderer, plantrenderer, (obstacle renderer?) derive from
public class Visualizer : MonoBehaviour
{
    public VisualizerAnimator animator;

    // public virtual void IncreaseSunLevel(int mod) {

    // }
    // public virtual void IncreaseWaterLevel(int mod) {

    // }
    // public virtual void SetSunLevel(int lvl) {
        
    // }
    // public virtual void SetWaterLevel(int lvl) {

    // }
    public Vector2 anchor;
    // identical methods, separate for clarity's sake
    public virtual void OnStateChanged(int newSun, int newWater) {

    }
    // identical methods, separate for clarity's sake
    public virtual void InitializeState(int sunLevel, int waterLevel) {

    }
}
