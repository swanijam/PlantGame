using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAction : MonoBehaviour
{
    [HideInInspector]
    public bool awaitingInput = false;
    public virtual void PrepareAction() {
        awaitingInput = true;
    }
    
    public delegate void OnTurnComplete();
    public event OnTurnComplete onTurnComplete;
    
    public virtual void CompleteTurn() {
        if (onTurnComplete != null) {
            onTurnComplete();
        }
    }
}
