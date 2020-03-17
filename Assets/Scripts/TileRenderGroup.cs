using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRenderGroup : MonoBehaviour
{
    public TileAnimator tileAnimator;
    public List<Visualizer> visualizers = new List<Visualizer>();

    // receives stateChangeEvents from tiles in the tileStateManager and passes them to renderers
    public void OnTileStateChanged(int sunLevel, int waterLevel) {
        for(int i = 0; i < visualizers.Count; i++) {
            visualizers[i].OnStateChanged(sunLevel, waterLevel);
        }
    }

    public void InitializeState(int sunLevel, int waterLevel) {
        for(int i = 0; i < visualizers.Count; i++) {
            visualizers[i].InitializeState(sunLevel, waterLevel);
        }
    }

    public void AddVisualizer(Visualizer visualizer) {
        visualizers.Add(visualizer);
    }
}