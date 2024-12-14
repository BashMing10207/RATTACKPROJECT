using System.Collections.Generic;
using UnityEngine;

public class AgentStat : MonoBehaviour,IGetCompoable,IAfterInitable
{
    [SerializeField]
    private List<StatSO> _stats = new List<StatSO>();
    private List<SetablePair<StatSO,int>> _modifierDeleteList = new List<SetablePair<StatSO, int>>();
    private Agent _agent;

    public void Initialize(GetCompoParent entity)
    {
        _agent = entity as Agent;
    }
    public void AfterInit()
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

        foreach (StatSO stat in _stats)
        {
            foreach(SetablePair<StatModifierSO,int> mod in stat.TempModifilerAndRemain)
            {
                mod.Second--;
                if(mod.Second <= 0)
                {
                    stat.TryRemoveModifier(mod.First);
                }
            }
        }
    }

    public StatSO GetStat(string StatName)
    {
        foreach (StatSO stat in _stats)
        {
            if(stat.StatName == StatName) return stat;
        }
        return null;
    }
    [ContextMenu("power")]
    public void ShowPower()
    {
        print(GetStat("Power").Value);
    }

}
