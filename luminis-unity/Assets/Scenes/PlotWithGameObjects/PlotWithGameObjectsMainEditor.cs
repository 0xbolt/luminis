using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlotWithGameObjectsMain))]
public class PlotWithgameObjectsMainEditor : Editor {
    PlotWithGameObjectsMain script;

    public override void OnInspectorGUI() {
        serializedObject.Update();
        script = (PlotWithGameObjectsMain) target;

        // Components
        CsvFilePicker();
        MaxObjectsSlider();
        ObjectRadiusSlider();
        ReadCsvAndCreateGameObjectsButton();

        serializedObject.ApplyModifiedProperties();
    }

    void CsvFilePicker() {
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

    void MaxObjectsSlider() {
        SerializedProperty maxObjectsProp = serializedObject.FindProperty("maxObjects");
        EditorGUILayout.PropertyField(maxObjectsProp, new GUIContent("Max Objects"));
    }

    void ObjectRadiusSlider() {
        SerializedProperty objectRadiusProp = serializedObject.FindProperty("objectRadius");
        EditorGUILayout.Slider(objectRadiusProp, 0.1f, 1.0f, new GUIContent("Object Radius"));
    }

    void ReadCsvAndCreateGameObjectsButton() {
        if (GUILayout.Button("Read CSV and Create Game Objects")) {
            script.ReadCsv();
        }
    }
}
