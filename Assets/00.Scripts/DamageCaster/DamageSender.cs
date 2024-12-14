using UnityEngine;

public class DamageSender : MonoBehaviour, IGetDamageable,IGetCompoable
{
    public Health Target { get; private set; }
    public void GetDamage(float damage)
    {
        Target.GetDamage(damage);
    }
    public void Initialize(GetCompoParent entity)
    {
        Target = entity.GetCompo<Health>(true);
    }
}
