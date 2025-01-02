using System;
using UnityEngine;

    public class BashUtils
    {
        public static Vector3 V2ToV3(Vector2 v)
        {
            return new Vector3(v.x, 0, v.y);
        }
    }
[Serializable]
public class SetablePair<T, T2> //c#�� Pair�� Tuple�� readonly�̱⿡ ���� ����
{
    public T First { get; set; }
    public T2 Second { get; set; }
    public SetablePair(T key, T2 val)
    {
        this.First = key;
        this.Second = val;
    }
}