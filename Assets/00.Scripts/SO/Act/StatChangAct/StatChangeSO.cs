using UnityEngine;

[CreateAssetMenu(fileName = "StatActSO", menuName = "SO/Act/StatActSO")]
public class StatChangeSO : ActSO
{
    [SerializeField]
    protected StatModifierSO _moidfier;

    [SerializeField] 
    protected int _keepingTurn = 10;
    public override void RunAct(Vector3 dir, GetCompoParent agent)
    {
        float strength = 1;
        StatModifierSO moidfier = _moidfier;

        if (_affectStat != null)
        {
            StatSO stat = agent.GetCompo<StatManager>().GetStat(_affectStat.name);
            strength = dir.magnitude * stat.Value; //���� ���� ��ġ�� �����ų �� �ִ� ����
        }

        moidfier.ModifierValue *= strength;

        agent.GetCompo<StatManager>().AddStatMod(_moidfier);//GetStat(_moidfier.TargetStat.StatName).TryAddTemponaryModifiler(moidfier);
    }
}
