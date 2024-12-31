using System;
using UnityEngine;
using UnityEngine.Events;

public class ActCommander : MonoBehaviour,IGetCompoable,IAfterInitable
{
    private GetCompoParent _manager;

    public float ActionPoint = 10f;

    public Action ActFail;

    public ActSO CurrentAct;

    public UnityEvent OnActChangeEvent;
    public void Initialize(GetCompoParent entity)
    {
        _manager = entity;
       
    }

    public void AfterInit()
    {
        _manager.GetCompo<AgentManager>(true).ActionExcutor += TrySkill;

    }
    protected void TrySkill(Vector3 dir)
    {
        ActSO act = CurrentAct;
        float power = Mathf.Clamp(dir.magnitude+act.MinCost, 0f, Mathf.Min(ActionPoint,act.MaxCost));

        if (power < act.MinCost)
        {
            ActFail?.Invoke();
            return;
        }

        _manager.GetCompo<SkillAnimator>().SetAnim(act.HashValue);
        _manager.GetCompo<SkillAnimator>().SetAnim("Attack");
        _manager.GetCompo<AgentManager>(true).SelectedUnit().GetCompo<AgentActCommander>().ExecuteAct(act, dir.normalized*power);

        ActionPoint -= power;
    }

    public void SetAct(ActSO act)
    {
        CurrentAct = act;

        OnActChangeEvent?.Invoke();
        ////�� �Ʒ��� ���Ŀ� �ٸ� ��ũ��Ʈ�� ����(�� �����ϴ�...)
        //GetCompo<PlayerActions>().
    }

}
