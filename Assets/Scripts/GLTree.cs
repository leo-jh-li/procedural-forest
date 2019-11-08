using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLTree : MonoBehaviour
{
    public Material mat;
    public List<Vector3> points;

    public void OnRenderObject ()
    {
        // GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.LINES);
        // GL.Color(mat.color);
        for (int i = 0; i < points.Count - 1; i += 2) {
            GL.Vertex(points[i]);
            GL.Vertex(points[i+1]);
        }
        GL.End();
        // GL.PopMatrix();
    }
}
