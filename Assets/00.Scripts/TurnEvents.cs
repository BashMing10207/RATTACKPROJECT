using UnityEngine;
using UnityEngine.Events;

public class TurnEvents : MonoBehaviour,IGetCompoable,IAfterInitable
{
    protected TurnManager _turnManager;
    protected GetCompoParent _parent;

    [SerializeField]
    protected int _cycle,_eventTurn;

    public UnityEvent OnEventInvoke;

    public void Initialize(GetCompoParent entity)
    {
        _parent = entity;
    }
    public void AfterInit()
    {
        _turnManager = _parent.GetCompo<TurnManager>();
    }

    public void OnTurnENd()
    {
        if (_turnManager.TurnCount % _cycle == _eventTurn)
            OnEventInvoke?.Invoke();

    }
}
