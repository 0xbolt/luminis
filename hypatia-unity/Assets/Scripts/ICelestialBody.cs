using UnityEngine;

interface ICelestialBodyGameObject {
    public void SetRadius(float radius);
    public void SetColor(Color color);
    public void SetZUncertainty(float zUncertainty);
    public void SetRadialUncertainty(float radialUncertainty);
}
