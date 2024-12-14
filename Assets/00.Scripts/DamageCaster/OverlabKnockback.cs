using System.Collections.Generic;
using UnityEngine;

public class OverlabKnockback : Knockbacker
{
    [SerializeField]
    private LayerMask _whatisTarget;
    [SerializeField]
    private float _size = 5f, _damage = 1, _power = 1000;
    private Collider[] colliders = new Collider[35];

    private void OnEnable()
    {
        Attack(_damage);
    }
    public void Attack(float damage)
    {
        if (Physics.OverlapSphereNonAlloc(transform.position, _size, colliders, _whatisTarget) > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                Vector3 attackdir = (transform.position - colliders[i].transform.position).normalized * _power;
                KnockbackCast(gameObject, attackdir);
            }

        }
    }

}
