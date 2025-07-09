using System;
using UnityEngine;

public class Utils {
    public static Mesh GetSphereMesh() {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        var mesh = sphere.GetComponent<MeshFilter>().sharedMesh;
        GameObject.DestroyImmediate(sphere);
        return mesh;
    }

    public static bool RayIntersectsSphere(Ray ray, Vector3 center, float radius) {
        throw new NotImplementedException();
    }
}
