using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (TestGeneration))]
public class TestGenerationEditor : Editor
{
    public override void OnInspectorGUI() {
        TestGeneration testGeneration = (TestGeneration) target;

        if (DrawDefaultInspector()) {
            if (testGeneration.autoUpdate) {
                testGeneration.TestRenderer();
            }
        }

        if (GUILayout.Button("Recurse")) {
            testGeneration.TestRecurse();
        }

        if (GUILayout.Button("Render")) {
            testGeneration.TestRenderer();
        }

        if (GUILayout.Button("Recurse & Render")) {
            testGeneration.RecurseAndRender();
        }

        if (GUILayout.Button("Clear All")) {
            testGeneration.ClearPlants();
        }
    }
}
