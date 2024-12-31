using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class NetworkOwnerShip : NetworkBehaviour
{
    [SerializeField]
    private int _ownerID;
    [SerializeField]
    private NetworkObject _network;

    private void OnEnable()
    {
        if (IsOwner)
        {
            Debug.Log("이미 소유하고 있습니다.");
            return;
        }

        RequestOwnershipChangeServerRpc(NetworkManager.Singleton.LocalClientId);
        _network.Spawn();
    }

    [ServerRpc(RequireOwnership = false)]
    public void RequestOwnershipChangeServerRpc(ulong clientId)
    {
        if (IsServer)
        {
            GetComponent<NetworkObject>().ChangeOwnership(clientId);
            Debug.Log($"클라이언트 {clientId}가 소유권을 가져갔습니다.");
        }
    }
}
