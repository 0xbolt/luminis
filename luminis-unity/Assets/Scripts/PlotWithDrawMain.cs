using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class PlotWithDrawMain : MonoBehaviour {
    public string csvFilename;
    public DrawMethod drawMethod = DrawMethod.DrawMesh;
    public float objectRadius = 1.0f;
    public int maxObjects = 1000;
    public int randomSeed = 42;
    public Mesh mesh;
    public Material material;
    public MaterialPropertyBlock mpb;
    private List<CelestialBodyCsvRow> _records;
    private const int AUTO_READ_CSV_MAX_SIZE = (int)100e6; // 100 MB

    public enum DrawMethod {
        DrawMesh,
        RenderMesh,
    }

    void OnEnable() {
        ReadCsv();
    }

    void Update() {
        Draw();
    }

    void Draw() {
        if (drawMethod == DrawMethod.DrawMesh) {
            DrawMesh();
        }
    }

    void DrawMesh() {
        if (_records == null) return;
        RenderParams rp = new RenderParams(material);
        foreach (var r in _records) {
            Graphics.RenderMesh(rp, mesh, 0, Matrix4x4.Translate(new Vector3(r.X, r.Y, r.Z)));
        }
    }

    void OnValidate() {
        Random.InitState(randomSeed);
        if (GetCsvSize() < AUTO_READ_CSV_MAX_SIZE) {
            ReadCsv();
            // CenterCamera();
        }
    }

    public void CenterCamera() {
        if (Camera.main == null) return;
        if (_records == null || _records.Count == 0) return;

        var min = new Vector3(_records[0].X, _records[0].Y, _records[0].Z);
        var max = min;
        foreach (var r in _records) {
            var v = new Vector3(r.X, r.Y, r.Z);
            min = Vector3.Min(min, v);
            max = Vector3.Max(max, v);
        }

        // Debug.Log($"centering camera! {min}, {max}");
        // var center = (min + max) * 0.5f;
        // var size = (max - min).magnitude;
        // var cam = Camera.main;
        // var lookAt = Vector3.zero;
        // var dir = (center - lookAt).normalized;
        // if (dir == Vector3.zero) dir = Vector3.back;
        // cam.transform.position = lookAt + dir * (size * 1.5f);
        // cam.transform.LookAt(lookAt);
        // cam.nearClipPlane = 0.01f;
        // cam.farClipPlane = size * 4f;
    }

    getB

    public long GetCsvSize() {
        var csvPath = GetCsvPath();
        var fileInfo = new FileInfo(csvPath);
        return fileInfo.Length;
    }

    public string GetCsvPath() {
        return Path.Combine(Application.streamingAssetsPath, csvFilename);
    }

    public void ReadCsv() {
        var csvPath = GetCsvPath();
        _records = CelestialBodyCsv.GetRecords(csvPath);
        Debug.Log($"Done reading '{csvFilename}' ({_records.Count} records)");

        EditorUtility.SetDirty(this);
    }

}
