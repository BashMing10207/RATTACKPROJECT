using System.Collections.Generic;
using UnityEngine;

public class AgentActCommander : MonoBehaviour, IGetCompoable
{
    private Agent _agent;
    public List<ActSO> OwnActs = new List<ActSO>();
    public Transform WeaponTrm; 

    public void Initialize(GetCompoParent entity)
    {
        _agent = entity as Agent;
    }

    public void ExecuteAct(ActSO act, Vector3 dir)
    {
        act.RunAct(dir, ref _agent);
    }

}
