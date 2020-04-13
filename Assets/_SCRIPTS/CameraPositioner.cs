using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositioner : MonoBehaviour
{
    public PGTileStateManager stateManager;
    public Camera camera;
    private void OnEnable()
    {
        transform.localPosition = new Vector3(stateManager.dimensions.x/2f * stateManager.tileWidth, 0f, stateManager.dimensions.x/2f * stateManager.tileWidth);
        camera.orthographicSize = stateManager.dimensions.x;
    }
}
