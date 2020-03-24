using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantLogic : MonoBehaviour
{
    int curSun;
    int curWater;
    public enum Need {Nothing, Sun, Water}
    public Need currentNeed = Need.Nothing;

    public void OnStateChanged(int newSun, int newWater) {
        Need thisChange = Need.Nothing;
        if (newSun != curSun) {
            thisChange = Need.Sun;
        }
        if (newWater != curWater) {
            thisChange = Need.Water;
        }
        if (thisChange.Equals(currentNeed)) {
            Grow();
        } else if (thisChange.Equals(Need.Nothing)) {
            // do nothing
        } else {
            Die();
        }
    }

    public void Grow(){

    }

    public void Die() {
        GameObject.Destroy(gameObject);
    }
}
