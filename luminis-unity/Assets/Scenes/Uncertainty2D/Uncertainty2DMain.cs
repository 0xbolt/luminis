using UnityEngine;

[ExecuteInEditMode]
public class Uncertainty2DMain : MonoBehaviour {
    public Material material;

    void OnRenderImage(RenderTexture source, RenderTexture destination) {
        Graphics.Blit(null, destination, material);
    }
};
