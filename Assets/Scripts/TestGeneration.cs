using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TestGeneration : MonoBehaviour
{
    public string testTree;
    public string recursedTree;
    public int iterations;

    // TODO: temp?
    public float branchLength;
    public float angle;
    public float growthSpeed;

    public bool autoUpdate;

    private PlantGenerator generator;

    private Dictionary<char, List<string>> testRules = new Dictionary<char, List<string>>();

    void Awake() {
        Debug.Log("TestGeneration Awake()");
        // AddRule('X', "F-[[X]+X]+F[+FX]-X");
        // AddRule('X', "F+[[X]-X]-F[-FX]+X");
        // AddRule('F', "FF");

        // AddRule('X', "[-X][+X]");
        // AddRule('X', "F[-X]");
        // AddRule('X', "F[+X]");
        // AddRule('X', "F[-X][X][+X]");
        // AddRule('F', "FF");
        generator = GetComponent<PlantGenerator>();
    }

    private void AddRule(char key, string value) {
        if (testRules.ContainsKey(key)) {
            testRules[key].Add(value);
        } else {
            testRules.Add(key, new List<string>() { value });
        }
    }

    public void TestRecurse() {
        recursedTree = generator.Recurse(testTree, testRules, iterations);
    }

    public void TestRenderer() {
        generator.DisplayPlant(testTree, Vector3.zero, branchLength, angle, growthSpeed);
    }

    public void RecurseAndRender() {
        TestRecurse();
        testTree = recursedTree;
        TestRenderer();
    }

    public void ClearPlants() {
        generator.ClearPlants();
    }
}
