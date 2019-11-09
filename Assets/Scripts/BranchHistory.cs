using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchHistory
{
    public Vector3 endPosition;
    public Vector3 rotation;

    public BranchHistory(Vector3 pos, Vector3 rot) {
        endPosition = pos;
        rotation = rot;
    }
}
