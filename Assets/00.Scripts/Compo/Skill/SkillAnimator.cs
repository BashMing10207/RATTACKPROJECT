using UnityEngine;
using UnityEngine.InputSystem;

public class SkillAnimator : MonoBehaviour,IGetCompoable,IAfterInitable
{
    [SerializeField]
    private Animator _anim;
    private GetCompoParent _agent;

    public void Initialize(GetCompoParent entity)
    {
        _agent = entity;
        
    }

    public void AfterInit()
    {
        _agent.GetCompo<AgentManager>(true).ActionExcutor += SetAnimPowerValue;
    }

    public void SetPos(Transform trm)
    {
        transform.SetPositionAndRotation(trm.position, trm.rotation);
    }
    public void SetAnim(int hash)
    {
        _anim.SetTrigger(hash);
    }
    public void SetAnim(string str)
    {
        _anim.SetTrigger(str);
    }

    public void AttackAnim()
    {
        _anim.SetTrigger("Attack");
    }

    private void SetAnimPowerValue(Vector3 dir)
    {
        _anim.SetFloat("Value",dir.magnitude);
    }


}
