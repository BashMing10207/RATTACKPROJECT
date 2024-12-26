using UnityEngine;

[CreateAssetMenu(fileName = "MoveActSO", menuName = "SO/Act/MoveActSO")]

public class MoveActSO : ActSO
{
    [SerializeField]
    private StatSO _targetstat;
    [SerializeField]
    private Vector3 _defaultVector = Vector3.zero;
    public override void RunAct(Vector3 dir, ref Agent agent)
    {
        if (_targetstat == null) return;
        StatSO stat = agent.GetCompo<AgentStat>().GetStat(_targetstat.StatName);
        if (stat == null) return;
        
        dir = (dir+_defaultVector).normalized*dir.magnitude;
        Vector3 direction =  dir * stat.Value; //�����̴� �Ϳ� ��ø�̳� �� ���� ��ġ�� �����ų �� �ִ� ����
        agent.GetCompo<StonePhysics>().AddForce(direction);

    }
}
