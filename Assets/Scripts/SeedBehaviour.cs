using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedBehaviour : MonoBehaviour
{
    public Seed seed;

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.CompareTag("Soil")) {
            // TODO: OPTIMIZE
            PlantGenerator gen = Object.FindObjectOfType<PlantGenerator>();
            string recursedTree = gen.Recurse(seed.axiom, seed.rulesDict, seed.iterations);
            gen.DisplayPlant(recursedTree, transform.position, seed.branchLength, seed.angle, seed.growthSpeed);
            Destroy(gameObject);
        }
    }
}
