using UnityEngine;

public abstract class DamageCaster : MonoBehaviour
{
    protected virtual void DamageCast(GameObject target, float damage)
    {
        if (target.TryGetComponent<IGetDamageable>(out IGetDamageable healthcompo))
        {
            healthcompo.GetDamage(damage);
        }
    }
}
