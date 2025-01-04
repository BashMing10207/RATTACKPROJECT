using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveActSO", menuName = "SO/Act/MoveActSO")]

public class MoveActSO : ActSO
{
    [SerializeField]
    private Vector3 _defaultVector = Vector3.zero;
    public override void RunAct(Vector3 dir,GetCompoParent agent)
    {
        
        dir = (dir+_defaultVector).normalized*dir.magnitude;
        Vector3 direction = dir * PlayerANDAgentStat(agent); //�����̴� �Ϳ� ��ø�̳� �� ���� ��ġ�� �����ų �� �ִ� ����
        agent.GetCompo<StonePhysics>().AddForce(direction);

    }
}
