using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class PlotWithRenderMeshMain : MonoBehaviour {
    public const int AUTO_READ_CSV_MAX_SIZE = (int)100e6; // 100 MB
    public string csvFilename;
    public float objectRadius = 1.0f;
    public int maxObjects = 1000;
    public int randomSeed = 42;
    public Mesh mesh;
    public Material material;

    public Color materialColor;
    public bool randomColor;
    public float lerpDDist = 1;

    // Render vars
    MaterialPropertyBlock mpb;

    // Object info
    List<CelestialBodyCsvRow> records;
    List<Color> colors = new List<Color>();
    string lastCsvFilename;

    void OnEnable() {
        mpb = new MaterialPropertyBlock();
        ReadCsv();

        // Set sphere as default mesh
        if (mesh == null) {
            mesh = Utils.GetSphereMesh();
        }
    }

    void Update() {
        DrawSphere();
    }

    void DrawSphere() {
        if (records == null) return;

        RenderParams rp = new RenderParams(material) {
            matProps = mpb
        };
        if (!randomColor) {
            mpb.SetColor("_Color", materialColor);
        }

        for (int i = 0; i < System.Math.Min(records.Count, maxObjects); i++) {
            var r = records[i];
            if (randomColor) {
                mpb.SetColor("_Color", colors[i]);
            }

            Vector3 xyz = CelestialBody.GetXYZ(r, useDDist: false);
            Vector3 xyzDDist = CelestialBody.GetXYZ(r, useDDist: true);
            Vector3 sigmaXyz = CelestialBody.GetSigmaXYZ(r);
            Debug.DrawLine(xyz - sigmaXyz, xyz + sigmaXyz);

            Vector3 pos = Vector3.Lerp(xyz, xyzDDist, lerpDDist);
            Vector3 scale = objectRadius * Vector3.one;
            var matrix = Matrix4x4.TRS(pos, Quaternion.identity, scale);
            Graphics.RenderMesh(rp, mesh, 0, matrix);
        }
        // DrawLine();
    }

    void DrawEllipsoid() {

    }

    void OnDrawGizmos() {
        // for (int i = 0; i < System.Math.Min(records.Count, maxObjects); i++) {
        //     var r = records[i];

        //     Vector3 xyz = CelestialBody.GetXYZ(r, useDDist: false);
        //     Vector3 sigmaXyz = CelestialBody.GetSigmaXYZ(r);

        //     Gizmos.DrawLine(xyz - sigmaXyz, xyz + sigmaXyz);
        // }
    }

    void OnGUI() {
        // if (GUILayout.Button("Press Me")) {
        //     Debug.Log("Hello!");
        // }
    }

    void OnValidate() {
        UnityEngine.Random.InitState(randomSeed);
        if (GetCsvSize() < AUTO_READ_CSV_MAX_SIZE && lastCsvFilename != csvFilename) {
            ReadCsv();
            CenterCamera();
        }

    }

    public void CenterCamera() {
        if (Camera.main == null) return;
        if (records == null || records.Count == 0) return;

        var min = CelestialBody.GetXYZ(records[0]);
        var max = min;
        foreach (var r in records) {
            var v = CelestialBody.GetXYZ(r);
            min = Vector3.Min(min, v);
            max = Vector3.Max(max, v);
        }
    }

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
        records = CelestialBodyCsv.GetRecords(csvPath);
        Debug.Log($"Done reading '{csvFilename}' ({records.Count} records)");

        colors.Clear();
        var matBufData = new List<Matrix4x4>();
        for (int i = 0; i < records.Count; i++) {
            var c = UnityEngine.Random.ColorHSV(0f, 1f, 0.55f, 0.65f, 0.9f, 1f);
            colors.Add(c);

            var r = records[i];
            Vector3 pos = CelestialBody.GetXYZ(r);
            Vector3 scale = objectRadius * Vector3.one;
            var matrix = Matrix4x4.TRS(pos, Quaternion.identity, scale);
            matBufData.Add(matrix);
        }

        lastCsvFilename = csvFilename;
        EditorUtility.SetDirty(this);
    }
}
