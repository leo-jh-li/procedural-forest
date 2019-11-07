using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float dropCooldown;
    private float currCooldown;
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
    }

    private void DropSeed() {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        
        // TODO
        Instantiate(testSeed, new Vector2(ray.origin.x, ray.origin.y), Quaternion.identity);
    }
}
