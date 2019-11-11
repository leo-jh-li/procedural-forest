using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RandomRangeFloat
{
    public float min;
    public float max;

    public RandomRangeFloat(float min, float max) {
        this.min = min;
        this.max = max;
    }

    public float GetRandom() {
        return Random.Range(min, max);
    }
}