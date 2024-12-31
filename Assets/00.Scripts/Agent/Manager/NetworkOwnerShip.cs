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
            Debug.Log("�̹� �����ϰ� �ֽ��ϴ�.");
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
            Debug.Log($"Ŭ���̾�Ʈ {clientId}�� �������� ���������ϴ�.");
        }
    }
}
