using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (TestPlantRenderer))]
public class TestPlantRendererEditor : Editor
{
    public override void OnInspectorGUI() {
        TestPlantRenderer testRenderer = (TestPlantRenderer) target;

        if (DrawDefaultInspector()) {
            if (testRenderer.autoUpdate) {
                testRenderer.TestRenderer();
            }
        }

        if (GUILayout.Button("Render")) {
            testRenderer.TestRenderer();
        }
    }
}
