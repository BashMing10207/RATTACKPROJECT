using UnityEngine;

[CreateAssetMenu(fileName = "MoveActSO", menuName = "SO/Act/MoveActSO")]

public class MoveActSO : ActSO
{
    [SerializeField]
    private StatSO _targetstat;
    public override void RunAct(Vector3 dir, ref Agent agent)
    {
        if (_targetstat == null) return;
        StatSO stat = agent.GetCompo<AgentStat>().GetStat(_targetstat.StatName);
        if (stat == null) return;
        
        Vector3 direction =  dir     * stat.Value; //�����̴� �Ϳ� ��ø�̳� �� ���� ��ġ�� �����ų �� �ִ� ����
        agent.GetCompo<StonePhysics>().AddForce(direction);
        Debug.Log(direction);
    }
}
