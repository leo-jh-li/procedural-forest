using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Seed", menuName = "Seed", order = 0)]
public class Seed : ScriptableObject {

    public new string name;
    public float branchLength;
    public Gradient gradient;
    public RulesetDict ruleset = new RulesetDict();
    
    // TODO: colour gradient, maybe seed shape/sprite, rules/ruleset
    // axiom??

}
