using System;
using Unity.Services.Analytics;
using UnityEngine;

public enum ActType
    {
        Projectile,
        Impact,
        Melee,
        Move,
        Buff,
        Passive
    }
//[CreateAssetMenu(fileName="SO/Act")]
[Serializable]
public abstract class ActSO : ScriptableObject
{

    public float MinCost = 1f, MaxCost = 10f;

    public string ActName = "";
    [Multiline]
    public string Description = "";

    public Sprite Icon;

    public string AnimParamName;
    public int HashValue;


    public ActType ActTypeEnum;
    public float MaxDistance = 10f;

    public float StatModValue = 0.1f;

    public int SKillCoollDown = 1;

    [SerializeField]
    protected StatSO _affectStat;
    private void OnValidate()
    {
        HashValue = Animator.StringToHash(AnimParamName);
    }

    public abstract void RunAct(Vector3 dir, GetCompoParent agent);

    protected float PlayerANDAgentStat(GetCompoParent agent)
    {
        if (_affectStat == null) return 1;
        StatSO stat = agent.GetCompo<StatManager>().GetStat(_affectStat.StatName);
        if (stat == null) return 1;

        if(agent.GetType() == typeof(Unit))
        {
            return  1+(stat.Value * (agent as Unit).MasterController.GetCompo<StatManager>().GetStat(_affectStat.StatName).Value)*StatModValue;
        }

        return stat.Value;
    }
}
