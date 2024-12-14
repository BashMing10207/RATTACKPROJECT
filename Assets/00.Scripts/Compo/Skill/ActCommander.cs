using System;
using UnityEngine;

public class ActCommander : MonoBehaviour,IGetCompoable
{
    private AgentManager _manager;

    public float ActionPoint = 10f;

    public Action ActFail;

    public void Initialize(GetCompoParent entity)
    {
        _manager = entity as AgentManager;
        _manager.ActionExcutor += TrySkill;

    }

    protected void TrySkill(Vector3 dir,ActSO act)
    {
        float power = Mathf.Clamp(dir.magnitude+act.MinCost, 0f, Mathf.Min(ActionPoint,act.MaxCost));

        if (power < act.MinCost)
        {
            ActFail?.Invoke();
            return;
        }

        _manager.SelectedUnit().GetCompo<AgentActCommander>().ExecuteAct(act, dir.normalized*power);

        ActionPoint -= power;
    }

}
