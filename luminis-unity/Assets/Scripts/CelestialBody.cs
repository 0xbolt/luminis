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

    public static Vector3 GetXYZ(CelestialBodyCsvRow cb, bool useDDist = true) {
        float raRad = Mathf.Deg2Rad * cb.Ra;
        float decRad = Mathf.Deg2Rad * cb.Dec;

        float dist = cb.Dist;
        if (useDDist) {
            dist += cb.DDist;
        }

        float x = dist * Mathf.Cos(decRad) * Mathf.Cos(raRad);
        float y = dist * Mathf.Cos(decRad) * Mathf.Sin(raRad);
        float z = dist * Mathf.Sin(decRad);

        return new Vector3(x, y, z);
    }

    public static Vector3 GetSigmaXYZ(CelestialBodyCsvRow cb) {
        float raRad = Mathf.Deg2Rad * cb.Ra;
        float decRad = Mathf.Deg2Rad * cb.Dec;

        float x = cb.SigmaDist * Mathf.Cos(decRad) * Mathf.Cos(raRad);
        float y = cb.SigmaDist * Mathf.Cos(decRad) * Mathf.Sin(raRad);
        float z = cb.SigmaDist * Mathf.Sin(decRad);

        return new Vector3(x, y, z);
    }
}
