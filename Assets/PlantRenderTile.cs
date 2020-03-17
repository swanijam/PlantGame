using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantRenderTile : RenderTile
{
    
    // identical methods, separate for clarity's sake
    public override void OnStateChanged(int newSun, int newWater) {
        Vector3 curScale = transform.localScale;
        curScale.y *= 2.0f;
        transform.localScale = curScale;
    }
}
