using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

[ExecuteInEditMode]
public class PlantGenerator : MonoBehaviour
{
    // public Dictionary<char, List<string>> rules = new Dictionary<char, List<string>>();
    
    public int iterations;

    public GameObject branchPrefab;

    //TODO: min/max, randomness?
    public float branchLength;
    public float angle;
    public float growthSpeed;
    public bool instantGrowth;

    // public bool runInEditMode;


    private List<GameObject> activeBranches = new List<GameObject>();

    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles) {
        return Quaternion.Euler(angles) * (point - pivot) + pivot;
    }

    private void Awake() {
        Debug.Log("PlantGenerator Awake()");
        // AddRule('X', "F-[[X]+X]+F[+FX]-X");
        // AddRule('X', "F+[[X]-X]-F[-FX]+X");
        // AddRule('F', "FF");

        // AddRule('X', "[-X][+X]");
        // AddRule('X', "F[-X]");
        // AddRule('X', "F[+X]");
        // AddRule('X', "F[-X][X][+X]");
        // AddRule('F', "FF");
    }

    // private void AddRule(char key, string value) {
    //     if (rules.ContainsKey(key)) {
    //         rules[key].Add(value);
    //     } else {
    //         rules.Add(key, new List<string>() { value });
    //     }
    // }

    // Returns a random rule for this key if one exists, or returns the key otherwise.
    private string GetRule(char key, Dictionary<char, List<string>> rules) {
        if (rules.ContainsKey(key)) {
            int randIndex = Random.Range(0, rules[key].Count);
            return rules[key][randIndex];
        }

        Debug.Log("no rule");

        return key.ToString();
    }

    public string Recurse(string str, RulesetDict rules, int iterations) {
        if (!rules.rulesLoaded) {
            rules.LoadRules();
        }
        Dictionary<char, List<string>> loadedRules = rules.rulesDict;

        // Debug.Log(loadedRules);
        foreach (KeyValuePair<char, List<string>> pair in loadedRules) {
            Debug.Log(pair.Key);
            Debug.Log(pair.Value);


        }
        // Debug.Log(loadedRules['F']);
        // Debug.Log(loadedRules['X']);


        return Recurse(str, loadedRules, iterations);
    }

    public string Recurse(string str, Dictionary<char, List<string>> rules, int iterations) {
        Debug.Log("recursing " + str + " " + iterations + " times");

        for (int i = 0; i < iterations; i++) {
            StringBuilder stringBuilder = new StringBuilder();
            for (int j = 0; j < str.Length; j++) {
                stringBuilder.Append(GetRule(str[j], rules));
            }
            str = stringBuilder.ToString();
        }
        Debug.Log("result: " + str);
        return str;
    }
    
    // // Recurse using rules defined by the rules member
    // public string ManualRecurse(string str, int iterations) {
    //     Debug.Log("recursing " + str + " " + iterations + " times");

    //     for (int i = 0; i < iterations; i++) {
    //         StringBuilder stringBuilder = new StringBuilder();
    //         for (int j = 0; j < str.Length; j++) {
    //             stringBuilder.Append(GetRule(str[j], rules));
    //         }
    //         str = stringBuilder.ToString();
    //     }
    //     Debug.Log("result: " + str);
    //     return str;
    // }

    public void DisplayPlant(string plant, Vector3 startPosition) {

        Vector3 currPos = startPosition;

        // TODO
        Stack<Transform> transformStack = new Stack<Transform>();

        Stack<Branch> branchStack = new Stack<Branch>();


        // Transform lastTransform = null;
        Vector3 rotation = Vector3.zero;

        // Branch prevBranch = null;
        // Create parent branch
        Branch parentBranch = Instantiate(branchPrefab, startPosition, Quaternion.identity).GetComponent<Branch>();
        parentBranch.Initialize(0, Vector3.zero, 0, true);
        activeBranches.Add(parentBranch.gameObject);
        Branch prevBranch = parentBranch;

        for (int i = 0; i < plant.Length; i++) {
            switch (plant[i]) {
                case 'F':
                    // TODO
                    // GameObject currentBranch = Instantiate(branchPrefab);

                    // if (currTransform != null) {
                        // currPos = currTransform.
                    // }
                    // GameObject newBranch = Instantiate(branchPrefab, currPos, Quaternion.identity, currTransform);
                    Branch newBranch;
                    if (prevBranch != null) {
                        // Build new branch off previous branch
                        newBranch = Instantiate(branchPrefab, prevBranch.worldEndPos, Quaternion.identity, prevBranch.transform).GetComponent<Branch>();
                        rotation += prevBranch.rotation;
                    } else {
                        // Create initial branch
                        newBranch = Instantiate(branchPrefab).GetComponent<Branch>();
                    }

                    // Vector3 endPos = currPos + Vector3.up * branchLength;
                    newBranch.Initialize(branchLength, rotation, growthSpeed, instantGrowth);
                    // newBranch.GetComponent<LineRenderer>().SetPositions(new Vector3[]{currPos, endPos});
                    // newBranch.GetComponent<LineRenderer>().SetPosition(1, Vector3.up * branchLength);

                    // lineRenderer.SetPosition(0, currPos);
                    // lineRenderer.SetPosition(1, currPos + Vector3.up * branchLength);
                    // currPos = newBranch.worldEndPos;
                    // if (currTransform != null) {
                    //     newBranch.transform.SetParent(currTransform);
                    // }
                    // newBranch.transform.Rotate(rotation, Space.Self);
                    // lastTransform = newBranch.transform;
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
