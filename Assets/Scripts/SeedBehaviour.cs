using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedBehaviour : MonoBehaviour
{
    public Seed seed;
    [SerializeField] private float maxInitialTorque;
    [HideInInspector] public PlantGenerator plantGenRef;

    private void Start() {
        GetComponent<Rigidbody2D>().AddTorque(Random.Range(-maxInitialTorque, maxInitialTorque));
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.CompareTag("Soil")) {
            string recursedTree = plantGenRef.Recurse(seed.axiom, seed.rulesDict, seed.iterations.GetRandom());
            plantGenRef.DisplayPlant(recursedTree, transform.position.x, seed.branchLength, seed.branchWidth, seed.angle, seed.growthSpeed, seed.GetColour());
            Destroy(gameObject);
        }
    }
}
