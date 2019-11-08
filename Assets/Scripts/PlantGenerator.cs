using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

[ExecuteInEditMode]
public class PlantGenerator : MonoBehaviour
{
    public GameObject branchPrefab;
    public bool instantGrowth;
    private List<GameObject> activeBranches = new List<GameObject>();

    public GameObject glTreePrefab;

    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles) {
        return Quaternion.Euler(angles) * (point - pivot) + pivot;
    }

    // Returns a random rule for this key if one exists, or returns the key otherwise.
    private string GetRule(char key, Dictionary<char, List<string>> rules) {
        if (rules.ContainsKey(key)) {
            int randIndex = Random.Range(0, rules[key].Count);
            return rules[key][randIndex];
        }

        // Debug.Log("no rule");

        return key.ToString();
    }

    public string Recurse(string str, Dictionary<char, List<string>> rules, int iterations) {
        // Debug.Log("recursing " + str + " " + iterations + " times");

        for (int i = 0; i < iterations; i++) {
            StringBuilder stringBuilder = new StringBuilder();
            for (int j = 0; j < str.Length; j++) {
                stringBuilder.Append(GetRule(str[j], rules));
            }
            str = stringBuilder.ToString();
        }
        // Debug.Log("result: " + str);
        return str;
    }

    public void DisplayPlant(string plant, Vector3 startPosition, float branchLength, float angle, float growthSpeed, Color colour) {
        Stack<Branch> branchStack = new Stack<Branch>();

        Vector3 rotation = Vector3.zero;

        // Create parent branch
        Branch parentBranch = Instantiate(branchPrefab, startPosition, Quaternion.identity).GetComponent<Branch>();
        parentBranch.Initialize(0, Vector3.zero, 0, colour, true);
        activeBranches.Add(parentBranch.gameObject);
        Branch prevBranch = parentBranch;

        for (int i = 0; i < plant.Length; i++) {
            switch (plant[i]) {
                case 'F':
                    Branch newBranch;
                    newBranch = Instantiate(branchPrefab, prevBranch.worldEndPos, Quaternion.identity, prevBranch.transform).GetComponent<Branch>();
                    rotation += prevBranch.rotation;
                    newBranch.Initialize(branchLength, rotation, growthSpeed, colour, instantGrowth);
                    prevBranch = newBranch;
                    activeBranches.Add(newBranch.gameObject);
                    rotation = Vector3.zero;
                    break;
                case '[':
                    branchStack.Push(prevBranch);
                    break;
                case ']':
                    prevBranch = branchStack.Pop();
                    break;
                case '+':
                    rotation += Vector3.forward * angle;
                    break;
                case '-':
                    rotation -= Vector3.forward * angle;
                    break;
            }
        }
        StartCoroutine(parentBranch.Grow());
    }

    // Alternative display method for plays: GL lines
    public void DisplayPlantGL(string plant, Vector3 startPosition, float branchLength, float angle, float growthSpeed) {
        Stack<BranchInfo> branchStack = new Stack<BranchInfo>();

        Vector3 rotation = Vector3.zero;
        BranchInfo prevBranch = new BranchInfo(startPosition, Vector3.zero);

        GLTree rootBranch = Instantiate(glTreePrefab, startPosition, Quaternion.identity).GetComponent<GLTree>();

        for (int i = 0; i < plant.Length; i++) {
            switch (plant[i]) {
                case 'F':
                    rotation += prevBranch.rotation;
                    Vector3 worldEndPos = RotatePointAroundPivot(prevBranch.endPosition + Vector3.up * branchLength, prevBranch.endPosition, rotation);
                    rootBranch.points.Add(prevBranch.endPosition);
                    rootBranch.points.Add(worldEndPos);
                    prevBranch = new BranchInfo(worldEndPos, rotation);
                    rotation = Vector3.zero;
                    break;
                case '[':
                    branchStack.Push(prevBranch);
                    break;
                case ']':
                    prevBranch = branchStack.Pop();
                    break;
                case '+':
                    rotation += Vector3.forward * angle;
                    break;
                case '-':
                    rotation -= Vector3.forward * angle;
                    break;
            }
        }
    }

    public void ClearPlants() {
        for (int i = 0; i < activeBranches.Count; i++) {
            DestroyImmediate(activeBranches[i]);
        }
        activeBranches.Clear();
    }
}
