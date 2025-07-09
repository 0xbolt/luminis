using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlotWithRenderMeshMain))]
public class PlotWithRenderMeshMainEditor : Editor {
    SerializedProperty csvFilenameProp;
    SerializedProperty maxObjectsProp;
    SerializedProperty objectRadiusProp;
    SerializedProperty randomSeedProp;
    SerializedProperty meshProp;
    SerializedProperty materialProp;
    SerializedProperty materialColorProp;
    SerializedProperty randomColorProp;
    SerializedProperty lerpDDistProp;
    PlotWithRenderMeshMain script;

    void OnEnable() {
        csvFilenameProp = serializedObject.FindProperty("csvFilename");
        maxObjectsProp = serializedObject.FindProperty("maxObjects");
        objectRadiusProp = serializedObject.FindProperty("objectRadius");
        randomSeedProp = serializedObject.FindProperty("randomSeed");
        meshProp = serializedObject.FindProperty("mesh");
        materialProp = serializedObject.FindProperty("material");
        materialColorProp = serializedObject.FindProperty("materialColor");
        randomColorProp = serializedObject.FindProperty("randomColor");
        lerpDDistProp = serializedObject.FindProperty("lerpDDist");
        script = (PlotWithRenderMeshMain)target;
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        // CSV File Popup
        CsvFilePopup();
        // Read CSV Button
        if (GUILayout.Button("Read CSV")) {
            script.ReadCsv();
        }
        // Max Objects Setter
        EditorGUILayout.PropertyField(maxObjectsProp, new GUIContent("Max Objects"));
        // Object Radius Slider
        EditorGUILayout.Slider(objectRadiusProp, 0.1f, 1.0f, new GUIContent("Object Radius"));
        // Random Seed Setter
        EditorGUILayout.PropertyField(randomSeedProp, new GUIContent("Random Seed"));
        // Mesh Setter
        EditorGUILayout.PropertyField(meshProp, new GUIContent("Mesh"));
        // Material Setter
        EditorGUILayout.PropertyField(materialProp, new GUIContent("Material"));
        // Material Color Setter
        EditorGUI.BeginDisabledGroup(randomColorProp.boolValue);
        EditorGUILayout.PropertyField(materialColorProp, new GUIContent("Material Color"));
        EditorGUI.EndDisabledGroup();
        // Random Color Check
        EditorGUILayout.PropertyField(randomColorProp, new GUIContent("Random Color"));
        // Lerp DDist Setter
        EditorGUILayout.Slider(lerpDDistProp, 0.1f, 1.0f, new GUIContent("Lerp DDist"));

        // Center Camera Button
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

    void CsvFilePopup() {
        var streamingAssetsPath = Application.streamingAssetsPath;
        var files = System.IO.Directory.GetFiles(streamingAssetsPath, "*.csv");
        for (int i = 0; i < files.Length; i++) {
            files[i] = System.IO.Path.GetFileName(files[i]);
        }
        var selected = Mathf.Max(0, System.Array.IndexOf(files, csvFilenameProp.stringValue));
        selected = EditorGUILayout.Popup("CSV File", selected, files);
        csvFilenameProp.stringValue = files.Length > 0 ? files[selected] : "";
    }
}
