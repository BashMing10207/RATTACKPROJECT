using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MultiGameManager : NetworkBehaviour
{
    public Action OnTurnEnd;

    public bool IsMyturn => IsMyTurn;

    public List<Player> PlayerManagerCompo=new List<Player>();

    public static MultiGameManager Instance;

    private NetworkVariable<int> currentPlayer = new NetworkVariable<int>(0); // 현재 턴의 플레이어
    public bool IsMyTurn => NetworkManager.Singleton.LocalClientId == (ulong)currentPlayer.Value;


    protected void Awake()
    {
        if (Instance == null) Instance = this;
        OnTurnEnd += TurnEnd;
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            currentPlayer.Value = 0; // 첫 번째 플레이어부터 시작
        }
    }

    private void TurnEnd()
    {
        if (!IsMyTurn) return;

        if (IsServer)
        {
            currentPlayer.Value = (currentPlayer.Value + 1) % 2; // 다음 플레이어로 턴 전환
        }
    }

    private void Update()
    {

    }
    private void FixedUpdate()
    {

    }
}