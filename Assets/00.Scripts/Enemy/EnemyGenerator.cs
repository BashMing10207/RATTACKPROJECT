using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour, IGetCompoable,IAfterInitable
{
    private GetCompoParent _parent;
    private EnemyManager _enemyManager;

    public EnemyGroupGroupSO CurrentGenEnemyList;

    [SerializeField] List<EnemyGroupGroupSO> _enemyGroups;
    public void AfterInit()
    {
        _enemyManager = _parent.GetCompo<EnemyManager>();

    }

    public void Initialize(GetCompoParent entity)
    {
        _parent = entity;
    }

    public void GenEnemyies(Vector3 pos,int Type)
    {

    }

}
