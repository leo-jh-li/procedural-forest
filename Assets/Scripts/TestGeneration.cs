using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TestGeneration : MonoBehaviour
{
    public string testTree;
    public string recursedTree;
    public int iterations;

    public float branchLength;
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
        // PlantGenerator generator = GetComponent<PlantGenerator>();
        recursedTree = generator.Recurse(testTree, testRules, iterations);
    }

    public void TestRenderer() {
        // PlantGenerator generator = GetComponent<PlantGenerator>();
        generator.branchLength = branchLength;
        generator.DisplayPlant(testTree, Vector3.zero);
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
