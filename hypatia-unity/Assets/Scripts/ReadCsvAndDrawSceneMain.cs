using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class ReadCsvAndDrawSceneMain: MonoBehaviour {
    private const int DrawWithGameObjectsMaxObjects = 50_000;
    private List<GameObject> gameObjectList = new List<GameObject>();

    public string csvFilename;
    public int maxObjects = 1_000;
    public float objectRadius = 1.0f;
    public DrawMethod drawMethod;
    List<CelestialBodyCsvRow> records = new List<CelestialBodyCsvRow>();

    public enum DrawMethod {
        GameObjects,
        RenderMeshIndirect,
    }

    void OnEnable() {
        ClearChildren();
    }

    void OnValidate() {
        if (drawMethod == DrawMethod.GameObjects) {
            foreach (var go in gameObjectList) {
                go.transform.localScale = objectRadius * new Vector3(1, 1, 1);
            }
        }
    }

    public void ReadCsv() {
        var csvPath = Path.Combine(Application.streamingAssetsPath, csvFilename);
        records = CelestialBodyCsv.GetRecords(csvPath);
        Debug.Log($"Done reading '{csvFilename}' ({records.Count()} records)");
    }

    public void Draw() {
        ClearChildren();
        ReadCsv();

        if (drawMethod == DrawMethod.GameObjects) {
            DrawWithGameObjects();
        } else if (drawMethod == DrawMethod.RenderMeshIndirect) {
            DrawWithRenderMeshIndirect();
        }
    }

    private void DrawWithGameObjects() {
        Debug.Log("Drawing with GameObjects...");

        if (maxObjects > DrawWithGameObjectsMaxObjects) {
            Debug.LogError($"Drawing with GameObjects method is capped at {DrawWithGameObjectsMaxObjects} maxObjects...");
            maxObjects = DrawWithGameObjectsMaxObjects;
        }

        Debug.Log($"Drawing {System.Math.Min(maxObjects, records.Count)} objects...");

        gameObjectList.Clear();
        for (int i = 0; i < maxObjects && i < records.Count; i++) {
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
        Debug.Log("Updating GameObjects...");
    }

    private void DrawWithRenderMeshIndirect() {

    }

    private void DrawWithGPU() {
        Debug.Log("Drawing with GPU...");
    }

    void DrawWithGraphicsDrawMesh() {
        // Graphics.DrawMesh()
    }

    void DrawWithGraphicsDrawMeshInstanced() {
        // Graphics.DrawMeshInstanced
    }

    void DrawWithGraphicsDrawMeshInstancedIndirect() {
        // Graphics.DrawMeshInstancedIndirect
    }

    void DrawWithGraphicsDrawMeshInstancedProcedural() {
        // Graphics.DrawMeshInstancedProcedural
    }

    void DrawWithGraphicsDrawProceduralIndirect() {
        // Graphics.Render
    }

    void DrawWithGraphicsRenderMeshIndirect() {
        // Graphics.RenderMeshIndirect()
    }

    void DrawWithGraphicsRenderMesh() {
        // Graphics.RenderMesh

    }

    void DrawWithGraphicsRenderMeshInstanced() {
        // Graphics.RenderMeshInstanced
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
