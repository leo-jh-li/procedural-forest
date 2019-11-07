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

    public void DisplayPlant(string plant, Vector3 startPosition, float branchLength, float angle, float growthSpeed) {
        Stack<Branch> branchStack = new Stack<Branch>();

        Vector3 rotation = Vector3.zero;

        // Create parent branch
        Branch parentBranch = Instantiate(branchPrefab, startPosition, Quaternion.identity).GetComponent<Branch>();
        parentBranch.Initialize(0, Vector3.zero, 0, true);
        activeBranches.Add(parentBranch.gameObject);
        Branch prevBranch = parentBranch;

        for (int i = 0; i < plant.Length; i++) {
            switch (plant[i]) {
                case 'F':
                    Branch newBranch;
                    if (prevBranch != null) {
                        // Build new branch off previous branch
                        newBranch = Instantiate(branchPrefab, prevBranch.worldEndPos, Quaternion.identity, prevBranch.transform).GetComponent<Branch>();
                        rotation += prevBranch.rotation;
                    } else {
                        // Create initial branch
                        newBranch = Instantiate(branchPrefab).GetComponent<Branch>();
                    }
                    newBranch.Initialize(branchLength, rotation, growthSpeed, instantGrowth);
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

    public void ClearPlants() {
        for (int i = 0; i < activeBranches.Count; i++) {
            DestroyImmediate(activeBranches[i]);
        }
        activeBranches.Clear();
    }
}
