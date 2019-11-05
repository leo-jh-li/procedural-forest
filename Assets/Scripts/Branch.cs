using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    private float currLength;
    public float maxLength;
    public Vector3 rotation;
    public Vector3 worldEndPos;
    public float growthSpeed;

    public void Initialize(float maxLength, Vector3 rotation, float growthSpeed, bool instantGrowth) {
        currLength = 0;
        this.maxLength = maxLength;
        this.rotation = rotation;
        this.growthSpeed = growthSpeed;
        // worldEndPos = transform.position + Vector3.up * maxLength;
        worldEndPos = PlantGenerator.RotatePointAroundPivot(transform.position + Vector3.up * maxLength, transform.position, rotation);

        // TODO
        // TODO - set linerenderer?
        // GetComponent<LineRenderer>().SetPositions(new Vector3[]{transform.position, worldEndPos});

        // GetComponent<LineRenderer>().SetPosition(1, worldEndPos);
        if (instantGrowth) { 
            GetComponent<LineRenderer>().SetPositions(new Vector3[]{transform.position, worldEndPos});
            
        } else {
            GetComponent<LineRenderer>().SetPositions(new Vector3[]{transform.position, transform.position});
            StartCoroutine("Grow");
        }
    }

    private IEnumerator Grow() {
        while (currLength != maxLength) {
            GetComponent<LineRenderer>().SetPosition(1, Vector3.Lerp(transform.position, worldEndPos, currLength / maxLength));
            currLength = Mathf.Min(currLength + growthSpeed, maxLength);
            // Debug.Log("growing: " + Vector3.Lerp(transform.position, worldEndPos, currLength / maxLength));
            // Debug.Log("lerp: " + (currLength / maxLength));
            // Debug.Log(currLength + " + " + growthSpeed + "/" + maxLength);
            // Debug.Log(Mathf.Min(currLength + growthSpeed, maxLength));



            yield return null;
        }
        GetComponent<LineRenderer>().SetPosition(1, worldEndPos);
    }
}
