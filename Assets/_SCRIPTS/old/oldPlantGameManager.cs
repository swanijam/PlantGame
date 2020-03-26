using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class oldPlantGameManager : MonoBehaviour
{
    public PGTileStateManager state;
    // public TileAnimationManager animationManager;
    public ApplyTetramino tetraminoPlacementInput;

    public TMP_Text turnCounterText;
    // public TMPro.TextMeshProUGUI turnCounterText; 

    private void Awake()
    {
        Reset();
        tetraminoPlacementInput.onTetraminoApplied += AdvanceTurn;
        
    }

    // reset with Shift+R
    public void Reset() {
        state.ClearState();
        state.Initialize();
        // rendering.Initialize();
        // animationManager.Initialize();
        StopAllCoroutines();
        StartCoroutine(GameRoutine());
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.R)) {
            Debug.Log("Resetting game");
            Reset();
        }
    }





    // turn-taking loop
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
                // wait for a turn move to be taken
                yield return null;
            }
            waiting = true;
            currentTurn++;
        }
        
    }
}
