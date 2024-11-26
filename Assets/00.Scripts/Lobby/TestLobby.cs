using Unity.Services.Core;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using System.Threading.Tasks;
using Unity.Networking.Transport.Relay;

public class TestLobby : MonoBehaviour
{
    public static string Code;

    void Awake()
    {
        NetGameMana.Instance.relayMana = this;
    }
    async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () => { };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
    
    public void HostStartMing()
    {
        CreateRelay();
    }

    public void JoinMing(string id)
    {
        JoinRelay(id);
    }


    async void CreateRelay()
    {
        try
        {
        Allocation allocation =  await RelayService.Instance.CreateAllocationAsync(2);

            Code = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

           RelayServerData relayServerData = new RelayServerData(allocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(
                relayServerData
                );
            NetworkManager.Singleton.StartHost();
        }
        catch(RelayServiceException e)
        {
            Debug.LogException(e);
        }
    }

    async void JoinRelay(string joinCode)
    {
        await UnityServices.InitializeAsync();

        try
        {
        JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            Code = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            RelayServerData relayServerData = new RelayServerData(allocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(
                relayServerData
                );

            NetworkManager.Singleton.StartClient();
        }
        catch(RelayServiceException e)
        {
            Debug.LogException(e);
        }
    }

    public async Task<string> StartHostWithRelay(int maxConnections = 5) // 아래는 유니티 도큐먼트 예제코드밍
    {
        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnections);
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(allocation, "dtls"));
        var joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
        return NetworkManager.Singleton.StartHost() ? joinCode : null;
    }
    public async Task<bool> StartClientWithRelay(string joinCode)
    {
        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        var joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode: joinCode);
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(joinAllocation, "dtls"));
        return !string.IsNullOrEmpty(joinCode) && NetworkManager.Singleton.StartClient();
    }
}
