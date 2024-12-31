using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileSkillSO", menuName = "SO/Act/SummonSO/ProjectileSKillSO")]
public class ProjectielActSO : SummonActSO
{
    public override void RunAct(Vector3 dir, ref Agent agent)
    {
        Projectile projectile = Instantiate(Perfab,agent.transform.position,Quaternion.identity) as Projectile;

        projectile.Init(agent.transform.position+dir.normalized*1.05f, dir, dir.magnitude);// agent.GetCompo<AgentStat>().);
    }
}
