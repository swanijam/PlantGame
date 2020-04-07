using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnActionApplyWeather : TurnAction
{
    public override void PrepareAction() {
        base.PrepareAction();
        ShapePositioning.instance.onPlaceShape -= CompleteTurn;
        ShapePositioning.instance.onPlaceShape += CompleteTurn;
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

    // called as a subscriber to an event on ShapePositioning, triggered when a shape is applied.
    public override void CompleteTurn() {
        base.CompleteTurn();
        // Debug.Log("completing turn", this);
        // note that ShapePositioning, the utility responsible for actually using a forecast shape, calls ApplyTetramino on its own.
        //   so here, we don't do any of that and just assume that's happening and the interaction is done.
        PlantGameManager.instance.ClearForecastShapeSelection();
        PlantGameManager.instance.AdvanceWeather();
        ShapePositioning.instance.onPlaceShape -= CompleteTurn;
        PGTileStateManager.instance.AdvanceTurnsForPlants();  
    }
}
