using UnityEngine;

[ExecuteInEditMode]
public class HypatiaCppTestSceneMain : MonoBehaviour {
    void OnEnable() {
        Debug.Log("Testing HypatiaCpp...");
        Debug.Assert(HypatiaCpp.sum(1, 2) == 3, "Sum should be 3");
        Debug.Log("Assert passed!");
    }
}
