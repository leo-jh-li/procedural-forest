using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RulesetDict
{
    public List<CharRules> characterRules;
    public Dictionary<char, List<string>> rulesDict = new Dictionary<char, List<string>>();
}
