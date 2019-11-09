using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Seed", menuName = "Seed", order = 0)]
public class Seed : ScriptableObject {

    public new string name;
    public float branchLength;
    public float branchWidth;
    public float angle;
    public float growthSpeed;
    public RandomRange iterations;

    public Gradient gradient;
    public string axiom;
    [SerializeField] private RulesetDict ruleset = new RulesetDict();
    [HideInInspector] public Dictionary<char, List<string>> rulesDict = new Dictionary<char, List<string>>();

    private void OnEnable() {
        LoadRules();
    }

    public void LoadRules() {
        foreach(CharRules charRules in ruleset.characterRules) {
            rulesDict.Add(charRules.character, charRules.rules);
        }
        Debug.Log("rules loaded");
    }

    public Color GetColour() {
        return gradient.Evaluate(Random.Range(0f, 1f));
    }
}
