using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour,IGetCompoable
{
    public int TurnCount = 0; //����ȭ �Ǿ�� ����װ� ���ϰ� ������ ����ϴϱ� �ۺ�.(private set �ϸ� ����ȭ�� �ȵ� �Ф�)

    public UnityEvent OnTurnEndEvent;

    private GetCompoParent _parent;

    private void Start()
    {
        GameManager.Instance.OnTurnEndEvent += TurnAdd;
    }

    public void TurnAdd()
    {
        TurnCount++;
        OnTurnEndEvent?.Invoke();
    }

    public void Initialize(GetCompoParent entity)
    {
        _parent = entity;
    }
}
