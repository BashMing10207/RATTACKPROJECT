using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BashUtils
{
    public static Vector3 V2ToV3(Vector2 v)
    {
        return new Vector3(v.x, 0, v.y);
    }
    public static void SmoothNormals(Mesh mesh)
    {
        var trianglesOriginal = mesh.triangles;
        var triangles = trianglesOriginal.ToArray();

        var vertices = mesh.vertices;

        var mergeIndices = new Dictionary<int, int>();

        for (int i = 0; i < vertices.Length; i++)
        {
            var vertexHash = vertices[i].GetHashCode();

            if (mergeIndices.TryGetValue(vertexHash, out var index))
            {
                for (int j = 0; j < triangles.Length; j++)
                    if (triangles[j] == i)
                        triangles[j] = index;
            }
            else
                mergeIndices.Add(vertexHash, i);
        }

        mesh.triangles = triangles;

        var normals = new Vector3[vertices.Length];

        mesh.RecalculateNormals();
        var newNormals = mesh.normals;

        for (int i = 0; i < vertices.Length; i++)
            if (mergeIndices.TryGetValue(vertices[i].GetHashCode(), out var index))
                normals[i] = newNormals[index];

        mesh.triangles = trianglesOriginal;
        mesh.normals = normals;
    }
}
[Serializable]
public class SetablePair<T, T2> //c#의 Pair와 Tuple은 readonly이기에 새로 만든
{
    public T First { get; set; }
    public T2 Second { get; set; }
    public SetablePair(T key, T2 val)
    {
        this.First = key;
        this.Second = val;
    }
}