using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnActionPlacePlant : TurnAction
{
    public Transform previewObject;
    public GameObject placePlantPrefab;
    public override void PrepareAction()
    {
        base.PrepareAction();
        // create plant preview object to follow cursor
        // enable touch drag movement of plant preview object
    }

    private void Update()
    {
        if (true) {
        // if (awaitingInput) {   
            if (previewObject != null) previewObject.position = PGTileTargeting.currentTileCenter;
            if (Input.GetMouseButtonUp(0)) {
                // place plant if there's no plant present already
                if (!(PGTileStateManager.instance.HasPlant(PGTileTargeting.instance.currentTile.x, PGTileTargeting.instance.currentTile.y)))
                {
                    CompleteTurn();
                }
            }
        }
    }

    public override void CompleteTurn() {
        base.CompleteTurn();
        // Debug.Log("Completing turn", this);
        // later this will run a seed placement routine/animation before destroying
        GameObject.Destroy(previewObject.gameObject);
        previewObject = null;
        GameObject newPlantGo = Instantiate(placePlantPrefab, PGTileTargeting.currentTileCenter, Quaternion.identity);
        newPlantGo.SetActive(true);
        PGTileStateManager.instance.tiles[PGTileTargeting.instance.currentTile.x][PGTileTargeting.instance.currentTile.y].currentPlant = newPlantGo.GetComponent<Plant>();  
    }
}
