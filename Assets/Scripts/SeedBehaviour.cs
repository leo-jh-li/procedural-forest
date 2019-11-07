using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedBehaviour : MonoBehaviour
{
    public Seed seed;
    private bool planted;

    private void OnTriggerEnter2D(Collider2D col) {
        if(!planted && col.gameObject.CompareTag("Soil")) {
            // TODO: OPTIMIZE
            PlantGenerator gen = Object.FindObjectOfType<PlantGenerator>();
            string recursedTree = gen.Recurse(seed.ruleset.axiom, seed.ruleset, 5);
            gen.DisplayPlant(recursedTree, transform.position);
            planted = true;
        }
    }
}
