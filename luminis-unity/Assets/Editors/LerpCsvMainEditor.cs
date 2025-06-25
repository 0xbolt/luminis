using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LerpCsvMain))]
public class LerpCsvMainEditor : Editor {
    SerializedProperty csvFilename0Prop;
    SerializedProperty csvFilename1Prop;
    SerializedProperty drawMethodProp;
    SerializedProperty maxObjectsProp;
    SerializedProperty objectRadiusProp;
    SerializedProperty randomSeedProp;
    SerializedProperty meshProp;
    SerializedProperty materialProp;
    SerializedProperty lerpTProp;
    LerpCsvMain script;

    void OnEnable() {
        csvFilename0Prop = serializedObject.FindProperty("csvFilename0");
        csvFilename1Prop = serializedObject.FindProperty("csvFilename1");
        lerpTProp = serializedObject.FindProperty("lerpT");
        drawMethodProp = serializedObject.FindProperty("drawMethod");
        maxObjectsProp = serializedObject.FindProperty("maxObjects");
        objectRadiusProp = serializedObject.FindProperty("objectRadius");
        randomSeedProp = serializedObject.FindProperty("randomSeed");
        meshProp = serializedObject.FindProperty("mesh");
        materialProp = serializedObject.FindProperty("material");
        script = (LerpCsvMain)target;
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        // CSV File Popup
        CsvFilePopup("CSV File 0", csvFilename0Prop);
        CsvFilePopup("CSV File 1", csvFilename1Prop);
        // Read CSV Button
        if (GUILayout.Button("Read CSV")) {
            script.ReadCsv();
        }
        // Lerp T Slider
        EditorGUILayout.Slider(lerpTProp, 0f, 1f, new GUIContent("Lerp T"));
        // Max Objects Setter
        EditorGUILayout.PropertyField(maxObjectsProp, new GUIContent("Max Objects"));
        // Object Radius Slider
        EditorGUILayout.Slider(objectRadiusProp, 0.1f, 1.0f, new GUIContent("Object Radius"));
        // Draw Method Popup
        EditorGUILayout.PropertyField(drawMethodProp, new GUIContent("Draw Method"));
        // // Random Seed Setter
        // EditorGUILayout.PropertyField(randomSeedProp, new GUIContent("Random Seed"));
        // // Mesh Setter
        // EditorGUILayout.PropertyField(meshProp, new GUIContent("Mesh"));
        // Material Setter
        EditorGUILayout.PropertyField(materialProp, new GUIContent("Material"));
        // // Center Camera Button
        if (GUILayout.Button("Center Camera")) {
            script.CenterCamera();
        }

        serializedObject.ApplyModifiedProperties();
    }

    void OnGUI() {
        GUI.BeginGroup(new Rect(10, 10, 300, 150));
        GUI.Box(new Rect(0, 0, 300, 150), "Debug Panel");

        GUI.Label(new Rect(10, 30, 280, 20), "FPS: " + (1.0f / Time.deltaTime).ToString("F2"));

        GUI.EndGroup();
    }

    void CsvFilePopup(string description, SerializedProperty prop) {
        var streamingAssetsPath = Application.streamingAssetsPath;
        var files = System.IO.Directory.GetFiles(streamingAssetsPath, "*.csv");
        for (int i = 0; i < files.Length; i++) {
            files[i] = System.IO.Path.GetFileName(files[i]);
        }
        var selected = Mathf.Max(0, System.Array.IndexOf(files, prop.stringValue));
        selected = EditorGUILayout.Popup(description, selected, files);
        prop.stringValue = files.Length > 0 ? files[selected] : "";
    }
}
