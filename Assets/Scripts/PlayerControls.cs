using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float dropCooldown;
    private float currCooldown;
    [SerializeField] private GameObject eraser;
    private bool eraserEnabled;
    private Camera cam;

    public GameObject testSeed;

    private void Start() {
        cam = Camera.main;
    }

    private void Update()
    {
        if (currCooldown > 0) {
            currCooldown -= Time.deltaTime;
        } else {
            if (Input.GetButton("Fire1")) {
                DropSeed();
                currCooldown = dropCooldown;
            }
        }
        if (Input.GetButton("Fire2")) {
            // TODO
            // EraserOn();
            eraser.SetActive(true);
            eraser.transform.position = GetMousePos(cam);
        }
    }

    public static Vector2 GetMousePos(Camera camera) {
        return camera.ScreenPointToRay(Input.mousePosition).origin;
    }

    // TODO
    // private void EraserOn() {
    //     if (!eraserEnabled) {
    //         eraser.SetActive(true);
    //         eraserEnabled = true;
    //     }
    // }

    private void DropSeed() {
        // Vector3 mousePos = GetMousePos();
        // TODO
        Instantiate(testSeed, GetMousePos(cam), Quaternion.identity);
    }
}
