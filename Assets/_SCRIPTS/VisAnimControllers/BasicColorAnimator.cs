using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicColorAnimator : VisualizerAnimator
{

    public override void Initialize()
    {
        StartCoroutine(InitializeRoutine());
    }

    private IEnumerator InitializeRoutine()
    {
        float animTime = 0;
        while(animTime <= 1)
        {
            animTime += Time.deltaTime/initialAnimLength;
            float newScaleVal = (initialAnimCurve.Evaluate(animTime) * initialAnimModifier);
            animationTarget.localScale = new Vector3(baseScale.x, baseScale.y * newScaleVal, baseScale.z);
            yield return null;
        }
    }

    public override void OnTeardown()
    {
        StartCoroutine(TeardownRoutine());
    }

    private IEnumerator TeardownRoutine()
    {
        float animTime = 0;
        while(animTime <= 1)
        {
            animTime += Time.deltaTime/teardownAnimLength;
            float newScaleVal = (teardownAnimCurve.Evaluate(animTime) * teardownAnimModifier);
            animationTarget.localScale = baseScale * newScaleVal;
            yield return null;
        }
    }

}