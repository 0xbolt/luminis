using UnityEditor.VersionControl;
using UnityEngine;

[ExecuteInEditMode]
public class ReadAndPlotCsvSceneMain: MonoBehaviour {
    public string csvFilename;

    void OnEnable() {
        Debug.Log($"csvFilename: {csvFilename}");
    }
}
