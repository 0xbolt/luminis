using UnityEngine;

class CelestialBody {
    public static Vector3 GetXYZ(float Ra, float Dec, float Dist) {
        float raRad = Mathf.Deg2Rad * (float) Ra;
        float decRad = Mathf.Deg2Rad * (float) Dec;

        float X = Dist * Mathf.Cos(decRad) * Mathf.Cos(raRad);
        float Y = Dist * Mathf.Cos(decRad) * Mathf.Sin(raRad);
        float Z = Dist * Mathf.Sin(decRad);

        return new Vector3(X, Y, Z);
    }
}
