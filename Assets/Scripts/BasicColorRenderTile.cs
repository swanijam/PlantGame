using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicColorRenderTile : RenderTile
{
    public Renderer renderer;
    public Vector2 sunLevelRange;
    public Vector2 sunBrightnessRange;
    public Vector2 waterRange;
    public Color dryColor, saturatedColor;
    
    // identical methods, separate for clarity's sake
    public override void OnStateChanged(int newSun, int newWater) {
        Color newColor =Color.Lerp(dryColor, saturatedColor, Mathf.InverseLerp(waterRange.x, waterRange.y, newWater));
        float sunMod = Mathf.Lerp(sunBrightnessRange.x, sunBrightnessRange.y, Mathf.InverseLerp(sunLevelRange.x, sunLevelRange.y, newSun));
        newColor = newColor * sunMod;//Mathf.Lerp(sunBrightnessRange.x, sunBrightnessRange.y, Mathf.InverseLerp(sunLevelRange.x, sunLevelRange.y, newSun));
        renderer.material.SetColor("_BaseColor", newColor);
    }
    // identical methods, separate for clarity's sake
    public override void InitializeState(int sunLevel, int waterLevel) {
        Color newColor =Color.Lerp(dryColor, saturatedColor, Mathf.InverseLerp(waterRange.x, waterRange.y, sunLevel));
        float sunMod = Mathf.Lerp(sunBrightnessRange.x, sunBrightnessRange.y, Mathf.InverseLerp(sunLevelRange.x, sunLevelRange.y, waterLevel));
        newColor = newColor * sunMod;//Mathf.Lerp(sunBrightnessRange.x, sunBrightnessRange.y, Mathf.InverseLerp(sunLevelRange.x, sunLevelRange.y, newSun));
        renderer.material.SetColor("_BaseColor", newColor);
    }
}
