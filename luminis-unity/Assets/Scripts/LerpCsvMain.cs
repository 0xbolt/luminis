using System.Collections.Generic;
using System.IO;
using UnityEngine;

[ExecuteInEditMode]
public class LerpCsvMain : MonoBehaviour {
    public string csvFilename0;
    public string csvFilename1;
    public float lerpT = 0;
    public float objectRadius = 1.0f;
    public int maxObjects = 1000;
    public int randomSeed = 42;
    public Mesh mesh;
    public Material material;
    public DrawMethod drawMethod = DrawMethod.DrawMesh;

    private List<Color> colors = new List<Color>();
    private List<CelestialBodyCsvRow> _records0;
    private List<CelestialBodyCsvRow> _records1;

    private MaterialPropertyBlock mpb;

    public enum DrawMethod {
        DrawMesh,
    }

    void OnEnable() {
        mpb = new MaterialPropertyBlock();
    }

    void Update() {
        if (drawMethod == DrawMethod.DrawMesh) {
            DrawMesh();
        }
    }

    public void ReadCsv() {
        var csvPath0 = Path.Combine(Application.streamingAssetsPath, csvFilename0);
        _records0 = CelestialBodyCsv.GetRecords(csvPath0);
        Debug.Log($"Done reading '{csvFilename0}' ({_records0.Count} records)");

        var csvPath1 = Path.Combine(Application.streamingAssetsPath, csvFilename1);
        _records1 = CelestialBodyCsv.GetRecords(csvPath1);
        Debug.Log($"Done reading '{csvFilename1}' ({_records1.Count} records)");

        if (_records0.Count != _records1.Count) {
            Debug.LogError("CSVs have different sizes!");
            return;
        }

        colors.Clear();
        for (int i = 0; i < _records0.Count; i++) {
            colors.Add(Random.ColorHSV());
        }
    }

    void DrawMesh() {
        if (_records0 == null || _records1 == null) return;
        for (int i = 0; i < _records0.Count; i++) {
            var r0 = _records0[i];
            var r1 = _records1[i];
            var pos0 = new Vector3(r0.X, r0.Y, r0.Z);
            var pos1 = new Vector3(r1.X, r1.Y, r1.Z);
            var pos = (1 - lerpT) * pos0 + lerpT * pos1;

            mpb.SetColor("_Color", colors[i]);
            RenderParams rp = new RenderParams(material) {
                matProps = mpb
            };
            Graphics.RenderMesh(rp, mesh, 0, Matrix4x4.Translate(pos));
        }
    }

    public void CenterCamera() {
        Debug.Log("CenterCamera not implemented :(");
    }
}
