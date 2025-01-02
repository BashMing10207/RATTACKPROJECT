using UnityEngine;

public class AgentForceMoveCompo : MonoBehaviour,IGetCompoable
{
    private GetCompoParent _parent;

    private Vector3 _targetPos = Vector3.zero;

    private float _moveTime;
    public void Initialize(GetCompoParent entity)
    {
        _parent = entity;
    }


    public void SetTargetPos(Vector3 pos)
    {
        _targetPos = pos;
    }

    public void SetMoveTime(float time)
    {
        _moveTime = time;
    }

    private void Update()
    {
        if(_moveTime > 0)
        {
            _parent.transform.position = Vector3.Lerp(_parent.transform.position, _targetPos, Time.deltaTime*20);
            
        _moveTime -= Time.deltaTime;
        }
    }
}
