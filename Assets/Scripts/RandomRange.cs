using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RandomRange
{
	public int min;
	public int max;
	
    public RandomRange(int min, int max) {
        this.min = min;
        this.max = max;
    }

    public int GetRandom()
    {
        int rando = Random.Range(min, max);
        Debug.Log(rando);
        return rando;
    }

    private void OnValidate() {
        Debug.Log("changed");
        if (min > max) {
            Debug.LogWarning("min of RandomRange is greater than max");
        } else if (max < min) {
            Debug.LogWarning("max of RandomRange is less than min");
        }
    }
}