using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileAnimationManager : MonoBehaviour
{
    public Transform animationTarget;
    public Vector3 baseScale;

    [Header ("Initial Animation")]
    public float initialAnimLength;
    public AnimationCurve initialAnimCurve;
      
    void Start()
    {
        // this gets initialized by the GameManager now
        baseScale = animationTarget.localScale;
        Initialize();   
    }

    public void Initialize()
    {
        StartCoroutine(InitialAnimationRoutine());
    }

    private IEnumerator InitialAnimationRoutine()
    {
        float animTime = 0;
        while(animTime <= 1)
        {
            animTime += Time.deltaTime/initialAnimLength;
            float newScaleVal = (initialAnimCurve.Evaluate(animTime)*2 * baseScale.x);
            animationTarget.localScale = new Vector3(newScaleVal, baseScale.y, newScaleVal);
            yield return null;
        }
    }

}