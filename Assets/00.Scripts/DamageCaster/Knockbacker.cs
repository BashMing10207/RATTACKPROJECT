using UnityEngine;

public abstract class Knockbacker : MonoBehaviour
{
    protected virtual void KnockbackCast(GameObject target, Vector3 dir)
    {
        if (target.TryGetComponent<IGetDamageable>(out IGetDamageable healthcompo))
        {
            (healthcompo as DamageSender).Target.Owner.GetCompo<StonePhysics>(true).AddForce(dir);
        }
    }
    protected virtual void KnockbackCast(GameObject target, Vector3 dir,Vector3 hitPoint)
    {
        if (target.TryGetComponent<IGetDamageable>(out IGetDamageable healthcompo))
        {
            (healthcompo as DamageSender).Target.Owner.GetCompo<StonePhysics>(true).AddForceAt(hitPoint,dir);
        }
    }
}

