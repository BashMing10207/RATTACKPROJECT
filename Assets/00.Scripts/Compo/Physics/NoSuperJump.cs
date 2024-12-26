using UnityEngine;

public class NoSuperJump : MonoBehaviour,IGetCompoable
{
    private Rigidbody _rbCompo;
    private Collider _collider;
    private Unit _agent;

    [SerializeField]
    private float _limitVelocity=5f,_divimod=1.5f,_maxDistance=0.41f;

    public void Initialize(GetCompoParent entity)
    {
        _agent = entity as Unit;
        _rbCompo = _agent.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(_agent.IsSelected)
        if (!Physics.Raycast(transform.position,Vector3.down,_maxDistance))
        {
            if (_rbCompo.linearVelocity.y > _limitVelocity)
            {
                _rbCompo.linearVelocity -= new Vector3(0, _rbCompo.linearVelocity.y / _divimod, 0);
            }
        }
    }
}
