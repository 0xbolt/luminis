using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class PlotWithGameObjectsMain : MonoBehaviour {
    public string csvFilename;
    public int maxObjects = 1_000;
    public float objectRadius = 1.0f;

    List<CelestialBodyCsvRow> records = new List<CelestialBodyCsvRow>();
    List<GameObject> gameObjectList = new List<GameObject>();

    void OnEnable() {
        ClearChildren();
    }

    void OnValidate() {
        UpdateGameObjects();
    }

    public void ReadCsv() {
        var csvPath = Path.Combine(Application.streamingAssetsPath, csvFilename);
        records = CelestialBodyCsv.GetRecords(csvPath);
        Debug.Log($"Done reading '{csvFilename}' ({records.Count()} records)");
    }

    public void CreateGameObjects() {
        ClearChildren();
        gameObjectList.Clear();

        var count = System.Math.Min(maxObjects, records.Count);
        Debug.Log($"Creating {count} Game Objects");

        for (int i = 0; i < count; i++) {
            var r = records[i];
            var go = CreateGameObject(r);
            gameObjectList.Add(go);
        }
    }

    GameObject CreateGameObject(CelestialBodyCsvRow r) {
        var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go.transform.SetParent(transform);
        go.transform.position = new Vector3(r.X, r.Y, r.Z);
        go.transform.localScale = objectRadius * new Vector3(1, 1, 1);
        var renderer = go.GetComponent<Renderer>();
        if (renderer != null) {
            renderer.sharedMaterial = new Material(renderer.sharedMaterial);
            renderer.sharedMaterial.color = Random.ColorHSV();
        }
        return go;
    }

    void UpdateGameObjects() {
        foreach (var go in gameObjectList) {
            go.transform.localScale = objectRadius * new Vector3(1, 1, 1);
        }
    }

    void ClearChildren() {
#if UNITY_EDITOR
        while (transform.childCount > 0) {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
#else
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
#endif
    }
}
