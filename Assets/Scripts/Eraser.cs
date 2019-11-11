using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : MonoBehaviour
{
    [SerializeField] private float eraserRadius;
    [SerializeField] private float minRadius;
    [SerializeField] private float maxRadius;
    [SerializeField] private ContactFilter2D branchFilter;
    [SerializeField] private LineRenderer ring;
    [SerializeField] private int ringSegments;


    private Camera cam;

    private void Start() {
        cam = Camera.main;
        UpdateEraserSize();
    }

    private void UpdateEraserSize() {
        ring.SetVertexCount(ringSegments + 1);
        float angle = 0;
        for (int i = 0; i < ringSegments + 1; i++) {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * eraserRadius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * eraserRadius;
            ring.SetPosition(i, new Vector3(x, y, 0));
            angle += (360 / ringSegments);
        }
    }

    private void Update() {
        Vector2 mousePos = PlayerControls.GetMousePos(cam);
        List<Collider2D> results = new List<Collider2D>();
        Physics2D.OverlapCircle(mousePos, eraserRadius, branchFilter, results);
        if (!Input.GetButton("Fire2")) {
            gameObject.SetActive(false);
        }
        foreach(Collider2D collider in results) {
            collider.transform.parent.GetComponent<Branch>().EraseBranch();
        }

        // Resize eraser on scroll input
        if (Input.mouseScrollDelta.y != 0) {
            eraserRadius = Mathf.Clamp(eraserRadius + Input.mouseScrollDelta.y, minRadius, maxRadius);
            UpdateEraserSize();
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, eraserRadius);
    }
}
