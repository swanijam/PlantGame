﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class faceCam : MonoBehaviour
{
    public Transform cameraTransform;

    // Update is called once per frame
    void Update()
    {
        transform.forward = -cameraTransform.forward;
    }
}
