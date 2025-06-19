using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ReadCsvAndDrawSceneMain))]
public class ReadCsvAndDrawSceneMainEditor: Editor {
    public override void OnInspectorGUI() {
        serializedObject.Update();
        var script = (ReadCsvAndDrawSceneMain) target;

        // CSV File Picker
        string streamingAssetsPath = Application.streamingAssetsPath;
        string[] files = System.IO.Directory.GetFiles(streamingAssetsPath, "*.csv");
        for (int i = 0; i < files.Length; i++) {
            files[i] = System.IO.Path.GetFileName(files[i]);
        }
        int selected = Mathf.Max(0, System.Array.IndexOf(files, script.csvFilename));
        selected = EditorGUILayout.Popup("CSV File", selected, files);
        script.csvFilename = files.Length > 0 ? files[selected] : "";
        EditorUtility.SetDirty(script);

        // Max Objects Setter
        SerializedProperty maxObjectsProp = serializedObject.FindProperty("maxObjects");
        EditorGUILayout.PropertyField(maxObjectsProp, new GUIContent("Max Objects"));

        // Object Radius Slider
        SerializedProperty objectRadiusProp = serializedObject.FindProperty("objectRadius");
        EditorGUILayout.Slider(objectRadiusProp, 0.1f, 1.0f, new GUIContent("Object Radius"));

        // Draw Method Picker
        string[] drawMethods = System.Enum.GetNames(typeof(ReadCsvAndDrawSceneMain.DrawMethod));
        script.drawMethod = (ReadCsvAndDrawSceneMain.DrawMethod) EditorGUILayout.Popup("Draw Method", 0, drawMethods);

        // Draw Button
        if (GUILayout.Button("Draw")) {
            script.ReadCsv();
            script.Draw();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
