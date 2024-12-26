using UnityEngine;

public class DieAndEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject _deadEffect;
    [SerializeField]
    private float _delayTime = 0;
    public virtual void OnDead()
    {
        OnDead(transform,transform.forward, 1);
    }

    public virtual void OnDead(Transform targetTrm, Vector3 direction, float scale)
    {
        if (_deadEffect != null)
        {
            Transform instance = Instantiate(_deadEffect, targetTrm.position, Quaternion.LookRotation(direction)).transform;
            instance.localScale *= scale;
        }
        Destroy(gameObject,_delayTime);
    }
}
