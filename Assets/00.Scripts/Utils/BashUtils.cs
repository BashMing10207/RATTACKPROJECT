using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

    public class BashUtils
    {
        public static Vector3 V2ToV3(Vector2 v)
        {
            return new Vector3(v.x, 0, v.y);
        }
    public static void SmoothNormals(Mesh m)
    {
        byte _useSmoothNormals = 1;

        NativeArray<Vector3> _vertices = new NativeArray<Vector3>(m.vertices, Allocator.Temp);
        NativeArray<Vector3> _normals = new NativeArray<Vector3>(m.normals, Allocator.Temp);
        NativeHashMap<float3, float3> _normalsUnityHash = new NativeHashMap<float3, float3>(m.normals.Length, Allocator.Temp);
        for (int v = 0; v < _vertices.Length; v += 3)
        {
            float3x3 vertex3 = new float3x3(_vertices[v], _vertices[v + 1], _vertices[v + 2]);
            float3 normalFlat = math.normalize(math.cross(vertex3[2] - vertex3[0], vertex3[1] - vertex3[0]));

            for (int n = 0; n < 3; n++)
            {
                float3 vertexHash = math.floor(vertex3[n] * 1000f + 0.5f) * 0.001f; // ROUNDS THE FLOAT3 TO THE 3rd DECIMAL PLACE, OTHERWISE IT'S NOT USABLE AS A KEY
                int count = v + n;

                switch (_useSmoothNormals)
                {
                    case 1:
                        switch (_normalsUnityHash.ContainsKey(vertexHash))
                        {
                            case true:
                                break;
                            case false:
                                _normalsUnityHash.Add(vertexHash, normalFlat);
                                break;
                        }

                        _normals[count] = _normalsUnityHash[vertexHash];
                        break;
                    case 0:
                        _normals[count] = normalFlat;
                        break;
                }
            }
        }

        _normalsUnityHash.Dispose();

        m.normals = _normals.ToArray();

        _normals.Dispose();
        _vertices.Dispose();
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