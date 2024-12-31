using System;
using System.Collections.Generic;
using Unity.Multiplayer.Playmode;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : GetCompoParent //Manager<GameManager>
{
    public Action OnTurnEnd;

    private bool _isPlayerturn =true;
    public bool IsPlayerturn => _isPlayerturn;

    public List<Player> PlayerManagerCompos = new List<Player>();
    public EnemyManager EnemyManagerCompo;
    public Player PlayerManagerCompo => PlayerManagerCompos[_isPlayerturn ? 0:1];

    public event Action OnTwoTurnEndEvent,OnTurnEndEvent;
    public static GameManager Instance;

protected override void Awake()
    {
        Instance = this;
        base.Awake();
        OnTurnEnd += TurnEnd;
    }

    private void TurnEnd()
    {
        PlayerManagerCompo.gameObject.SetActive(false);
        _isPlayerturn = !_isPlayerturn;//턴넘기기
        PlayerManagerCompo.gameObject.SetActive(true);
        OnTurnEndEvent?.Invoke();

        if(_isPlayerturn)
            OnTwoTurnEndEvent?.Invoke();
        
    }

    private void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            TurnEnd();
        }
    }
    private void FixedUpdate()
    {
        
    }
}
