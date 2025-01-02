using System;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour, IGetCompoable//GetCompoParent // : Manager<AgentManager>
{
    public List<Unit> Units = new List<Unit>();

    public int SelectedUnitIdx = 0;

    public event Action<Vector3> ActionExcutor;

    protected GetCompoParent _parent;

    [SerializeField]
    private Unit _unitprefab;

    public virtual void Initialize(GetCompoParent entity)
    {
        _parent = entity;
        print(entity.gameObject.name);
    }

    protected virtual void Start()
    {
        //Units.ForEach(unit => unit.Init(_parent));
        foreach (Unit unit in Units)
        {
            unit.Init(_parent);
            print(unit.gameObject.name);

        }
    }
    public Unit SelectedUnit() => Units[SelectedUnitIdx];
    protected virtual void GetAction(Vector3 dir)
    {
        ActionExcutor?.Invoke(dir);
    }

    public void CreateStone(Vector3 pos)
    {
        AddStone(Instantiate(_unitprefab, pos, Quaternion.identity));
    }

    public void AddStone(Unit unit)
    {
        Units.Add(unit);
        unit.Init(_parent);
    }

    public void UnitDie(Unit unit)
    {
        Units.Remove(unit);

        Destroy(unit.gameObject);
    }
}

