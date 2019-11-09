using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float dropCooldown;
    private float currCooldown;
    [SerializeField] private GameObject eraser;
    private bool eraserEnabled;
    [SerializeField] private GameObject seedPrefab;
    [SerializeField] private Seed[] seeds;
    private Camera cam;

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
            eraser.SetActive(true);
            eraser.transform.position = GetMousePos(cam);
        }
    }

    public static Vector2 GetMousePos(Camera camera) {
        return camera.ScreenPointToRay(Input.mousePosition).origin;
    }

    private Seed GetRandomSeed() {
        return seeds[Random.Range(0, seeds.Length)];
    }

    private void DropSeed() {
        GameObject seedObj = Instantiate(seedPrefab, GetMousePos(cam), Quaternion.identity);
        seedObj.GetComponent<SeedBehaviour>().seed = GetRandomSeed();
    }
}
