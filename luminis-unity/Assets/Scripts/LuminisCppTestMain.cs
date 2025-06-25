using UnityEngine;

[ExecuteInEditMode]
public class LuminisCppTestSceneMain : MonoBehaviour {
    void OnEnable() {
        Debug.Log("Testing LuminisCpp...");
        Debug.Assert(LuminisCpp.sum(1, 2) == 3, "Sum should be 3");
        Debug.Log("Assert passed!");
    }
}
