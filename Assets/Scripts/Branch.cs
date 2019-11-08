using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public bool grown;
    private float currLength;
    public float maxLength;
    public Vector3 rotation;
    public Vector3 worldEndPos;
    public float growthSpeed;
    [SerializeField] private CapsuleCollider2D collider;

    public void Initialize(float maxLength, Vector3 rotation, float growthSpeed, Color colour, bool instantGrowth) {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startColor = colour;
        lineRenderer.endColor = colour;
        grown = false;
        currLength = 0;
        this.maxLength = maxLength;
        this.rotation = rotation;
        collider.transform.eulerAngles = rotation;
        this.growthSpeed = growthSpeed;
        worldEndPos = PlantGenerator.RotatePointAroundPivot(transform.position + Vector3.up * maxLength, transform.position, rotation);
        if (instantGrowth) {
            lineRenderer.enabled = true;
            lineRenderer.SetPositions(new Vector3[]{transform.position, worldEndPos});
            collider.enabled = true;
            // Place collider in centre of branch and with proper length
            collider.transform.localPosition = worldEndPos - transform.position;
            collider.size = new Vector2(collider.size.x, maxLength);
            grown = true;
        } else {
            lineRenderer.SetPositions(new Vector3[]{transform.position, transform.position});
        }
    }

    public IEnumerator Grow() {
        lineRenderer.enabled = true;
        while (currLength != maxLength) {
            Vector3 currEndPos = Vector3.Lerp(transform.position, worldEndPos, currLength / maxLength);
            lineRenderer.SetPosition(1, currEndPos);
            currLength = Mathf.Min(currLength + growthSpeed * Time.deltaTime, maxLength);
            collider.enabled = true;
            collider.transform.localPosition = currEndPos - transform.position;
            collider.size = new Vector2(collider.size.x, currLength);
            yield return null;
        }
        lineRenderer.SetPosition(1, worldEndPos);
        grown = true;
        foreach(Transform childTransform in transform)
        {
            // Debug.Log("grow");
            Branch childBranch = childTransform.GetComponent<Branch>();
            if (childBranch != null && !childBranch.grown) {
                StartCoroutine(childBranch.Grow());
            }
        }
    }

    public void EraseBranch() {
        Destroy(gameObject);
    }
}
