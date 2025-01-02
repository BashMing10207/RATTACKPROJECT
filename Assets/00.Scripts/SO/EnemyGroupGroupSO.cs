using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyGroup", menuName = "SO/EnemyGroup")]
public class EnemyGroupGroupSO : ScriptableObject
{
    [SerializeField]
    private List<EnemyGroup> Units = new(); 

    public Unit IndexOfUnit(int idx)
    {
        return Units[idx].Units[UnityEngine.Random.Range(0, Units[idx].Units.Count)];
    }
}
[Serializable]
public class EnemyGroup
{
    public List<Unit> Units = new();
}