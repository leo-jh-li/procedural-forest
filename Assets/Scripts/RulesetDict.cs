using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RulesetDict
{
    public string axiom;
    public List<CharRules> characterRules;
    public Dictionary<char, List<string>> rulesDict = new Dictionary<char, List<string>>();
    public bool rulesLoaded;

    public void LoadRules() {
        foreach(CharRules charRules in characterRules) {
            rulesDict.Add(charRules.character, charRules.rules);
        }
        rulesLoaded = true;
        Debug.Log("rules loaded");
    }
}
