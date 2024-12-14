
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IGetDamageable,IGetCompoable
{
    public float MaxHealth, CurrentHealth;

    public GetCompoParent Owner;

    public UnityEvent OnDead, OnHurt;
    public void Initialize(GetCompoParent entity)
    {
        Owner = entity;
        Init();
    }

    private void Init()
    {
        CurrentHealth = MaxHealth;
    }

    public void GetDamage(float damage)
    {
        CurrentHealth -= damage;

        Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        OnHurt?.Invoke();

        if (CurrentHealth == 0)
        {
            OnDead?.Invoke();
        }
    }

}
