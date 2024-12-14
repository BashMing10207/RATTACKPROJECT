using UnityEngine;
using UnityEngine.Events;

public class Projectile : SummonedObject
{
    RaycastHit _hit;
    
    protected float _speedMulti = 1, _sizeModify=1,_sizeModifyMulti=1;
    [SerializeField]
    private float _maxTime = 40;
    [SerializeField]
    private LayerMask _targetLayer;
    [SerializeField]
    private ProjectileSO _soData;

    public UnityEvent<IGetDamageable> OnAttackEvent;
    public UnityEvent OnDeadEvent;

    private float _time = 0;

    public override void Init(Vector3 startPos, Vector3 forwardDirection, float speedMultif)
    {
        transform.forward = forwardDirection;
        transform.position = startPos;
        _time = 0;
        _speedMulti = speedMultif;
    }

    public virtual void Update()
    {
        if (Physics.SphereCast(transform.position,_soData.radius,transform.forward,out _hit,_soData.speed*Time.deltaTime*_speedMulti,_targetLayer))
        {
            if (_hit.transform.TryGetComponent<IGetDamageable>(out IGetDamageable targetCompo))
            {
               OnAttackEvent?.Invoke(targetCompo);
            }
            Die();
        }

        transform.position = transform.position + transform.forward*_soData.speed*Time.deltaTime*_speedMulti;
        _time += Time.deltaTime;

        if(_time > _maxTime)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        OnDeadEvent?.Invoke();
    }
}
