using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnActionApplyWeather : TurnAction
{
    public override void PrepareAction() {
        base.PrepareAction();
        // create tetramino dynamic preview object
    }

    private void Update()
    {
        if (awaitingInput) {
            // if a forecastshape is selected
            // reposition tetramino according to mouseposition
            // rotate tetramino when screen is tapped
            // place tetramino on tap and hold
            // on place, CompleteTurn();
        }
    }

    public override void CompleteTurn() {
        // remove chosen tetramino from list
        // update plant needs
    }
}
