using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// basic game manager. Initializes tilestate and tilevisualrendering and then starts the game.
public class GameManager : MonoBehaviour
{
    public TileStateManager state;
    public TileVisualRenderer rendering;
    private void Awake()
    {
        Reset();
    }

    public void Reset() {
        state.Initialize();
        StartCoroutine(rendering.Initialize());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            Debug.Log("Resetting game");
            Reset();
        }
    }
}
