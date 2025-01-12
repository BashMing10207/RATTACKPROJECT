using System;
using Unity.Netcode;
using UnityEngine;

public class NetActCommander : NetworkBehaviour, IGetCompoable,IAfterInitable
{
    private GetCompoParent _manager;

    public float ActionPoint = 10f;

    public Action ActFail;

    public ActSO CurrentAct;

    public void Initialize(GetCompoParent entity)
    {
        _manager = entity;
        

    }
    public void AfterInit()
    {
        _manager.GetCompo<AgentManager>(true).ActionExcutor += TrySkillHandle;
    }
    public void TrySkillHandle(Vector3 dir)
    {
        TrySkillServerRpc(dir);
    }

    [ServerRpc(RequireOwnership = false)]
    protected void TrySkillServerRpc(Vector3 dir)
    {
        ActSO act = CurrentAct;
        _manager.GetCompo<SkillAnimator>().SetAnim(act.HashValue);
        _manager.GetCompo < PlayerActions >().AttackAnim();

        float power = Mathf.Clamp(dir.magnitude + act.MinPower, 0f, Mathf.Min(ActionPoint, act.MaxPower));

        if (power < act.MinPower)
        {
            ActFail?.Invoke();
            return;
        }

        _manager.GetCompo<AgentManager>(true).SelectedUnit().GetCompo<AgentActCommander>().ExecuteAct(act, dir.normalized * power);

        ActionPoint -= power;
    }

}

