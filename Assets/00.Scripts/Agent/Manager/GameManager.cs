using System;
using UnityEngine;

public class GameManager : Manager<GameManager>
{
    public Action OnTurnEnd;

    private bool _isPlayerturn;
    public bool IsPlayerturn => _isPlayerturn;

    public PlayerManager PlayerManagerCompo;
    public EnemyManager EnemyManagerCompo;

    protected override void Awake()
    {
        base.Awake();
        OnTurnEnd += TurnEnd;
    }

    private void TurnEnd()
    {
        _isPlayerturn = !_isPlayerturn;//�ϳѱ��
    }

    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        
    }
}
