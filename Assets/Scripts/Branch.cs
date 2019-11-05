using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    public float maxLength;
    public Vector3 rotation;
    public Vector3 worldEndPos;

    public void Initialize(float maxLength, Vector3 rotation) {
        this.maxLength = maxLength;
        this.rotation = rotation;
        // worldEndPos = transform.position + Vector3.up * maxLength;
        worldEndPos = PlantRenderer.RotatePointAroundPivot(transform.position + Vector3.up * maxLength, transform.position, rotation);

        // TODO
        GetComponent<LineRenderer>().SetPositions(new Vector3[]{transform.position, worldEndPos});
        // GetComponent<LineRenderer>().SetPosition(1, worldEndPos);
    }
}
