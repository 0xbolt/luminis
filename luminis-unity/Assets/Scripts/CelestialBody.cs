using UnityEngine;

class CelestialBody {
    public float ra;
    public float dec;
    public float dist;
    public float distSigma;

    public Vector3 GetXYZ() {
        float raRad = Mathf.Deg2Rad * ra;
        float decRad = Mathf.Deg2Rad * dec;

        float X = dist * Mathf.Cos(decRad) * Mathf.Cos(raRad);
        float Y = dist * Mathf.Cos(decRad) * Mathf.Sin(raRad);
        float Z = dist * Mathf.Sin(decRad);

        return new Vector3(X, Y, Z);
    }

    public static Vector3 GetXYZ(float ra, float dec, float dist) {
        float raRad = Mathf.Deg2Rad * ra;
        float decRad = Mathf.Deg2Rad * dec;

        float x = dist * Mathf.Cos(decRad) * Mathf.Cos(raRad);
        float y = dist * Mathf.Cos(decRad) * Mathf.Sin(raRad);
        float z = dist * Mathf.Sin(decRad);

        return new Vector3(x, y, z);
    }
}
