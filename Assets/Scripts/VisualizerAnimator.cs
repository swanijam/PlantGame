using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizerAnimator : MonoBehaviour
{
    public Transform animationTarget;
    public Vector3 baseScale;
    public Vector3 basePosition;
    public Quaternion baseRotation;

    [Header ("Initial Animation")]
    public float initialAnimLength;
    public float initialAnimModifier;
    public AnimationCurve initialAnimCurve;

    [Header ("Teardown Animation")]
    public float teardownAnimLength;
    public float teardownAnimModifier;
    public AnimationCurve teardownAnimCurve;
      
    private void Awake()
    {
        // this gets initialized by the GameManager now
        baseScale = animationTarget.localScale;
        basePosition = animationTarget.localPosition;
        baseRotation = animationTarget.localRotation;
    }

    public virtual void Initialize()
    {
        // Do initial stuff
    }

    public virtual void OnTeardown()
    {
        // Do teardown stuff
    }

}