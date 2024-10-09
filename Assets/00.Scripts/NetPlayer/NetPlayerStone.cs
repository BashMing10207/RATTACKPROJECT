using System;
using Unity.Netcode;
using UnityEngine;

public class NetPlayerStone : NetStone
{
    public Transform pivot;
    public GameObject outLine;
    
    public int id;
    public bool isHost;
    
    public float health;
    public float weight;
    public float force;
    public Action invokeActions;
    
    private void Awake()
    {
        NetPlayerMana.OnTurnEnd += HandleOnTurn;
        
    }
    private void Start()
    {
        if (NetGameMana.H_ISMULTI())
        {
            NetPlayerMana.stones[isHost?0:1].Add(this);
        }
        else
        {
            NetGameMana.Instance.playerOff.stones.Add(this);
        }
        
        outLine.SetActive(false);
    }
    private void HandleOnTurn()
    {
        print("HandleOnTurn");
        invokeActions?.Invoke();
    }
    public override void ForceMove(Vector3 dir, float power, float damage)
    {
        base.ForceMove(dir, power*this.power, damage);
    }

    private void OnDisable()
    {
        NetGameMana.Instance.dieSo.Play();
        NetPlayerMana.OnTurnEnd -= HandleOnTurn;
        if (NetGameMana.H_ISMULTI())
        {
            if (IsOwner)
            {
                NetPlayerMana.stones[isHost ? 0 : 1].Remove(this);
                NetGameMana.Instance.player.CamChangeServerRpc();
            }
            
        }
        else
        {
            NetGameMana.Instance.playerOff.stones.Remove(this);
        }
        if (IsOwner)
        {
            if (NetPlayerMana.stones[isHost ? 0 : 1].Count < 0)
            {
                NetGameMana.Instance.win.SetActive(true);
                winServerRpc();
                NetGameMana.Instance.win.SetActive(false);
                NetGameMana.Instance.lose.SetActive(true);
            }

        }
    }
    [ServerRpc]
    void winServerRpc()
    {
        NetGameMana.Instance.win.SetActive(true);
    }
    private void Update()
    {
        //print("ming");

        if (transform.position.y < -2)
        {
            Destroy(gameObject);
        }
        if(IsServer)
       ServerUpdateServerRpc();
    }

    [ServerRpc]
    void ServerUpdateServerRpc()
    {
        rb.AddForceAtPosition(Vector3.down * Time.deltaTime * NetGameMana.Instance.gravity, transform.position + -transform.up);
    }
    public void AddHealth(float amount)
    {
        health += amount;
    }
    
    public void AddForce(float amount)
    {
        power += amount;
    }
    
    public void AddWeight(float amount)
    {
        mass += amount;
    }
    
}
