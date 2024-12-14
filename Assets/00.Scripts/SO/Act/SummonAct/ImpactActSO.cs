using UnityEngine;

[CreateAssetMenu(fileName = "ImpactSkillSO", menuName = "SO/Act/SummonSO/ImpactSKillSO")]
public class ImpactActSO : SummonActSO
{
    public float AddSizeModifier = 0.1f;

    public override void RunAct(Vector3 dir, ref Agent agent)
    {
        SummonImpact impact = Instantiate(Perfab,agent.transform.position,Quaternion.identity) as SummonImpact;

        impact.Init(agent.transform.position, dir, AddSizeModifier);// agent.GetCompo<AgentStat>().);
    }
}
