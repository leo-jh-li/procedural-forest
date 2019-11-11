using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [HideInInspector] public bool grown;
    private float currLength;
    [HideInInspector] public float maxLength;
    [HideInInspector] public float width;
    [HideInInspector] public Vector3 rotation;
    // TODO
    public Vector3 worldEndPos;
    [HideInInspector] public float growthSpeed;
    [SerializeField] private CapsuleCollider2D collider;

    public void Initialize(float maxLength, float width, Vector3 rotation, float growthSpeed, Color colour, bool instantGrowth) {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startColor = colour;
        lineRenderer.endColor = colour;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
        grown = false;
        currLength = 0;
        this.maxLength = maxLength;
        this.width = width;
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
            collider.size = new Vector2(Mathf.Max(width, 0.01f), maxLength);
            grown = true;
        } else {
            collider.size = new Vector2(Mathf.Max(width, 0.01f), 0);
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
            collider.transform.position = (currEndPos + transform.position) / 2;
            collider.size = new Vector2(collider.size.x, currLength);
            yield return null;
        }
        lineRenderer.SetPosition(1, worldEndPos);
        collider.transform.position = (worldEndPos + transform.position) / 2;
        grown = true;
        foreach(Transform childTransform in transform)
        {
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
