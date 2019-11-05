using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlantRenderer : MonoBehaviour
{
    public string testTree;
    public float branchLength;
    public bool autoUpdate;

    void Start() {
        TestRenderer();
    }

    public void TestRenderer() {
        

        PlantRenderer renderer = GetComponent<PlantRenderer>();

        renderer.CleanPlants();

        renderer.branchLength = branchLength;
        renderer.DisplayPlant(testTree, Vector3.zero);
    }
}
