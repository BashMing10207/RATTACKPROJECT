using UnityEngine;
using UnityEngine.InputSystem;

public class SkillAnimator : MonoBehaviour,IGetCompoable
{
    [SerializeField]
    private Animator _anim;
    private PlayerManager _playerManager;

    public void Initialize(GetCompoParent entity)
    {
        _playerManager = entity as PlayerManager;
        _playerManager.ActionExcutor += SetAnimPowerValue;
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
    private void SetAnimPowerValue(Vector3 dir, ActSO agent)
    {
        _anim.SetFloat("Value",dir.magnitude);
    }

}
