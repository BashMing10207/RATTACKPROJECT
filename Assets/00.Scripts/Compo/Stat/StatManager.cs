using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class StatOverride
{
    public StatSO Stat;
    public bool IsOverride;
    public float OVerrideValue;
}

public class StatManager : MonoBehaviour,IGetCompoable
{
    [SerializeField]
    private List<StatOverride> _stats = new ();
    private List<SetablePair<StatSO,int>> _modifierDeleteList = new List<SetablePair<StatSO, int>>();
    protected GetCompoParent _parent;

    public void Initialize(GetCompoParent entity)
    {
        _parent = entity;
    }

    private void Init()
    {
        foreach (StatOverride stat in _stats)
        {
            if (stat.IsOverride)
            {
                stat.Stat.SetBaseValue(stat.OVerrideValue);
            }
        }
    }

    public void Start()
    {
        GameManager.Instance.OnTurnEnd += RemoveTempStat;
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnTurnEnd -= RemoveTempStat;
    }

    private void RemoveTempStat()
    {
        //_modifierDeleteList.

        foreach (StatOverride stat in _stats)
        {
            foreach(SetablePair<StatModifierSO,int> mod in stat.Stat.TempModifilerAndRemain)
            {
                mod.Second--;
                if(mod.Second <= 0)
                {
                    stat.Stat.TryRemoveModifier(mod.First);
                }
            }
        }
    }

    public StatSO GetStat(string StatName)
    {
        foreach (StatOverride stat in _stats)
        {
            if(stat.Stat.StatName == StatName) return stat.Stat;
        }
        return null;
    }

    public virtual void AddStatMod(StatModifierSO statMod)
    {
        GetStat(statMod.TargetStat.StatName).TryAddTemponaryModifiler(statMod);
    }
}
