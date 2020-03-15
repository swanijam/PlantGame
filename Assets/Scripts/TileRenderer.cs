using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///abstract class from which soilrenderer, plantrenderer, (obstacle renderer?) derive from
public class TileRenderer : MonoBehaviour
{
    public virtual void IncreaseSunLevel(int mod) {

    }
    public virtual void IncreaseWaterLevel(int mod) {

    }
    public virtual void SetSunLevel(int lvl) {
        
    }
    public virtual void SetWaterLevel(int lvl) {

    }
}
