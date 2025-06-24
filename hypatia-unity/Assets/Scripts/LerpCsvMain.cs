using UnityEngine;

[ExecuteInEditMode]
public class LerpCsvMain : MonoBehaviour {
    public string csvFilename0;
    public string csvFilename1;
    public float lerpT = 0;
    public DrawMethod drawMethod = DrawMethod.DrawMesh;

    public Mesh mesh;
    public Material material;

    public enum DrawMethod {
        DrawMesh,
    }

    void Update() {
        if (drawMethod == DrawMethod.DrawMesh) {
            DrawMesh();
        }
    }

    void DrawMesh() {
        Graphics.DrawMesh(mesh, Vector3.zero, Quaternion.identity, material, 0);
    }
}
