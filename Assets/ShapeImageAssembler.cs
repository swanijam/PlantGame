using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeImageAssembler : MonoBehaviour
{
    public GameObject tilePrefab;
    public Transform tileGroup;
    public Vector2 tileDimensions;
    public float margins;

    public ForecastShape _shape;
    public ForecastShape shape {
        get {
            return _shape;
        }
        set {
            _shape = value;
            UpdateVisual();
        }
    }
    List<GameObject> tilesTemp;
    
    public bool activateSelf = false;
    // Start is called before the first frame update
    void OnEnable()
    {
        if(activateSelf) UpdateVisual();
    }

    public void UpdateVisual () {
        if (tilesTemp == null) {
            tilesTemp = new List<GameObject>();
        } else {
            for (int i = 0; i < tilesTemp.Count; i++) {
                GameObject go = tilesTemp[i];
                tilesTemp[i] = null;
                GameObject.Destroy(go);
            }
        }
        if (shape.tiles.Count == 0) {
            Debug.Log("shape tiles 0 length??");
            return;
        }
        int minx = 5, miny = 5, maxx = -1, maxy = -1; 
        for (int x = 0; x < shape.tiles.Count; x++) {
            Vector2Int offset = shape.tiles[x] .editorOffset;
            if (offset.x < minx) minx = offset.x;
            if (offset.y < miny) miny = offset.y;
            if (offset.x > maxx) maxx = offset.x;
            if (offset.y > maxy) maxy = offset.y;
        }
        int spanx = maxx-minx + 1;
        int spany = maxy-miny + 1;
        spanx = Mathf.Max(spanx, spany);
        spany = Mathf.Max(spanx, spany);
        Vector2 adjustedTileDimensions = new Vector2(tileDimensions.x * (5f/spanx), tileDimensions.y * (5f/spany));
        RectTransform group = tileGroup.gameObject.GetComponent<RectTransform>();
        group.offsetMin = new Vector2(-adjustedTileDimensions.x * minx, -adjustedTileDimensions.y*miny);
        if (activateSelf) Debug.Log(adjustedTileDimensions, this);
        if (activateSelf) Debug.LogFormat("{0}/{1}/{2}/{3}/{4}/{5}/{6}/{7}", spanx, spany, group.offsetMin, minx, miny, maxx, maxy, this);

        for (int x = 0; x < shape.tiles.Count; x++) {
            Vector2Int offset = shape.tiles[x] .editorOffset;
            GameObject go = Instantiate(tilePrefab, Vector3.zero, Quaternion.identity, tileGroup);
            go.SetActive(true);
            RectTransform r = go.GetComponent<RectTransform>();
            r.anchorMax = Vector2Int.zero;
            r.anchorMin = Vector2Int.zero;
            r.offsetMin = new Vector2(offset.x * adjustedTileDimensions.x, offset.y * adjustedTileDimensions.y);
            r.offsetMax = Vector2.zero;
            r.sizeDelta = adjustedTileDimensions;
            tilesTemp.Add(go);
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < tilesTemp.Count; i++) {
            GameObject go = tilesTemp[i];
            tilesTemp[i] = null;
            GameObject.Destroy(go);
        }
    }
}
