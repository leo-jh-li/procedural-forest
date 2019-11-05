using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantRenderer : MonoBehaviour
{
    public GameObject branchPrefab;

    //TODO: min/max, randomness?
    public float branchLength;
    public float angle;

    private List<GameObject> activeBranches;

    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles) {
        return Quaternion.Euler(angles) * (point - pivot) + pivot;
    }

    public void DisplayPlant(string plant, Vector3 startPosition) {

        Vector3 currPos = startPosition;

        // TODO
        Stack<Transform> transformStack = new Stack<Transform>();

        Stack<Branch> branchStack = new Stack<Branch>();


        Transform lastTransform = null;
        Vector3 rotation = Vector3.zero;

        Branch lastBranch = null;


        for (int i = 0; i < plant.Length; i++) {

            Debug.Log(plant[i]);


            switch (plant[i]) {
                case 'F':
                    // TODO
                    // GameObject currentBranch = Instantiate(branchPrefab);

                    // if (currTransform != null) {
                        // currPos = currTransform.
                    // }
                    // GameObject newBranch = Instantiate(branchPrefab, currPos, Quaternion.identity, currTransform);
                    Branch newBranch;
                    if (lastBranch != null) {
                        newBranch = Instantiate(branchPrefab, currPos, Quaternion.identity, lastBranch.transform).GetComponent<Branch>();

                    } else {
                        newBranch = Instantiate(branchPrefab, currPos, Quaternion.identity).GetComponent<Branch>();
                    }

                    // Vector3 endPos = currPos + Vector3.up * branchLength;
                    newBranch.Initialize(branchLength, rotation);
                    // newBranch.GetComponent<LineRenderer>().SetPositions(new Vector3[]{currPos, endPos});
                    // newBranch.GetComponent<LineRenderer>().SetPosition(1, Vector3.up * branchLength);

                    // lineRenderer.SetPosition(0, currPos);
                    // lineRenderer.SetPosition(1, currPos + Vector3.up * branchLength);
                    currPos = newBranch.worldEndPos;
                    // if (currTransform != null) {
                    //     newBranch.transform.SetParent(currTransform);
                    // }
                    // newBranch.transform.Rotate(rotation, Space.Self);
                    // lastTransform = newBranch.transform;
                    lastBranch = newBranch;

                    activeBranches.Add(newBranch.gameObject);
                    rotation = Vector3.zero;
                    break;
                case '[':
                    branchStack.Push(lastBranch);
                    break;
                case ']':
                    lastBranch = branchStack.Pop();
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

    public void CleanPlants() {
        for (int i = 0; i < activeBranches.Count; i++) {
            DestroyImmediate(activeBranches[i]);
        }
        activeBranches.Clear();
    }
}
