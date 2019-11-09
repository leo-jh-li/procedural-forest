﻿using System.Collections;
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

    public int GetRandom() {
        return Random.Range(min, max + 1);
    }
}