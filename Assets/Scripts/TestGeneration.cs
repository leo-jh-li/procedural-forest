using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGeneration : MonoBehaviour
{
    public string testTree;
    public string recursedTree;
    public int iterations;

    public float branchLength;
    public bool autoUpdate;

    void Start() {
        // TestRenderer();
    }

    public void TestRecurse() {
        PlantGenerator generator = GetComponent<PlantGenerator>();

        recursedTree = generator.Recurse(testTree, iterations);
    }

    public void TestRenderer() {


        PlantGenerator generator = GetComponent<PlantGenerator>();

        generator.CleanPlants();

        generator.branchLength = branchLength;
        generator.DisplayPlant(testTree, Vector3.zero);
    }
}
