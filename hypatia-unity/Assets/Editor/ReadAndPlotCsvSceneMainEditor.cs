using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ReadAndPlotCsvSceneMain))]
public class ReadAndPlotCsvSceneMainEditor: Editor {
    public override void OnInspectorGUI() {
        var script = (ReadAndPlotCsvSceneMain) target;
        string streamingAssetsPath = Application.streamingAssetsPath;
        string[] files = System.IO.Directory.GetFiles(streamingAssetsPath, "*.csv");
        for (int i = 0; i < files.Length; i++) {
            files[i] = System.IO.Path.GetFileName(files[i]);
        }
        int selected = Mathf.Max(0, System.Array.IndexOf(files, script.csvFilename));
        selected = EditorGUILayout.Popup("CSV File", selected, files);
        script.csvFilename = files.Length > 0 ? files[selected] : "";
        EditorUtility.SetDirty(script);
    }
}
