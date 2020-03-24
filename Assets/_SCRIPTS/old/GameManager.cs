using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// basic game manager. Initializes tilestate and tilevisualrendering and then starts the game.
public class GameManager : MonoBehaviour
{
    public TileStateManager state;
    public TileVisualRenderer rendering;
    public TileAnimationManager animationManager;
    
    public ApplyTetramino tetraminoPlacementInput;
    public TMP_Text turnCounterText;
    // public TMPro.TextMeshProUGUI turnCounterText; 

    private void Awake()
    {
        Reset();
        tetraminoPlacementInput.onTetraminoApplied += AdvanceTurn;
        
    }

    public void Reset() {
        state.Initialize();
        rendering.Initialize();
        animationManager.Initialize();
        StopAllCoroutines();
        StartCoroutine(GameRoutine());
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.R)) {
        //     Debug.Log("Resetting game");
        //     Reset();
        // }
    }

    public void AdvanceTurn() {
        waiting = false;
    }

    public int _currentTurn = 1;
    public int currentTurn {
        get { return _currentTurn;}
        set {
            _currentTurn = value;
            if (turnCounterText != null) {
                turnCounterText.text = "Turn: "+_currentTurn;
            }
        } 
    }
    bool waiting = true;
    Coroutine gameRoutine;
    private IEnumerator GameRoutine() {
        while (true) {
            while (waiting) {
                yield return null;
            }
            waiting = true;
            currentTurn++;
        }
        
    }
}
