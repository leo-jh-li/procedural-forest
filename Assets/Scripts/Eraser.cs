using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : MonoBehaviour
{
    [SerializeField] private float eraserRadius;
    [SerializeField] private ContactFilter2D branchFilter;
    // TODO
    [SerializeField] private Sprite ring;
    private Camera cam;

    private void Start() {
        cam = Camera.main;
        UpdateEraserSize();
    }

    private void UpdateEraserSize() {
        eraserRadius *= transform.localScale.x;
    }

    private void Update() {
        Vector2 mousePos = PlayerControls.GetMousePos(cam);
        // transform.position = mousePos;
        List<Collider2D> results = new List<Collider2D>();
        Physics2D.OverlapCircle(mousePos, eraserRadius, branchFilter, results);
        if (!Input.GetButton("Fire2")) {
            gameObject.SetActive(false);
        }
        foreach(Collider2D collider in results) {
            collider.transform.parent.GetComponent<Branch>().EraseBranch();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, eraserRadius);
    }
}
