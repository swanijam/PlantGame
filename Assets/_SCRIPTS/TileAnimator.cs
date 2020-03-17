using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileAnimator : MonoBehaviour
{
    public Transform animationTarget;
    public Vector3 baseScale;
    public Vector3 basePosition;
    public Quaternion baseRotation;

    [Header ("Initial Animation")]
    public float initialAnimLength;
    public AnimationCurve initialAnimCurve;

    [Header ("Teardown Animation")]
    public float teardownAnimLength;
    public AnimationCurve teardownAnimCurve;
      
    void Start()
    {
        // this gets initialized by the GameManager now
        baseScale = Vector3.one;
        basePosition = animationTarget.localPosition;
        baseRotation = animationTarget.localRotation;
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
            float newScaleVal = (initialAnimCurve.Evaluate(animTime)*2);
            animationTarget.localScale = baseScale * newScaleVal;
            yield return null;
        }
    }

    public virtual void OnTeardown()
    {
        StartCoroutine(InitialAnimationRoutine());
    }

    private IEnumerator TeardownAnimationRoutine()
    {
        float animTime = 0;
        while(animTime <= 1)
        {
            animTime += Time.deltaTime/teardownAnimLength;
            float newScaleVal = (teardownAnimCurve.Evaluate(animTime)*2);
            animationTarget.localScale = baseScale * newScaleVal;
            yield return null;
        }
    }

}