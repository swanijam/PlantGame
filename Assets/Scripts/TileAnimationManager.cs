using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileAnimationManager : MonoBehaviour
{
    public TileStateManager state;
    public TileVisualRenderer tileRenderer;

    [Header ("Initial Animation")]
    public float staggerDelay = 0.2f;
    public float stepPause = 0.3f;

    public void Initialize()
    {
        StartCoroutine(ExecuteInitialAnimations());
    }

    private IEnumerator ExecuteInitialAnimations()
    {
        for(int x = 0; x < state.dimensions.x; x++)
        {
            for(int y = 0; y < state.dimensions.y; y++)
            {
                tileRenderer.tiles[x,y].tileAnimator.Initialize();
                yield return new WaitForSeconds(staggerDelay);
            }
        }
        yield return new WaitForSeconds(stepPause);

        for(int x = 0; x < state.dimensions.x; x++)
        {
            for(int y = 0; y < state.dimensions.x; y++)
            {
                // FIGURE OUT A BETTER WAY TO STAGE THESE
                for(int i = 0; i < tileRenderer.tiles[x,y].visualizers.Count; i++)
                {
                    tileRenderer.tiles[x,y].visualizers[i].animator.Initialize();
                }
                yield return new WaitForSeconds(staggerDelay);
            }
        }
    }

}