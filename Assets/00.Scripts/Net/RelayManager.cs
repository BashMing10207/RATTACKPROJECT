using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.Events;

public class RelayManager : MonoBehaviour
{
    public static RelayManager Instance;

    [SerializeField]
    private TMP_InputField _input;
    [SerializeField]
    private TextMeshProUGUI _text;

    public UnityEvent OnTwoPlayerEntered;

    private bool _started = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    private async void Start()
    {
        await InitializeUnityServicesAsync();
    }

    private async System.Threading.Tasks.Task InitializeUnityServicesAsync()
    {
        try
        {
            await UnityServices.InitializeAsync();
            Debug.Log("Unity Services 초기화 완료");

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                Debug.Log("익명 인증 성공");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Unity Services 초기화 중 오류 발생: {e.Message}");
        }
    }
    public void CreateRelayHandle()
    {
        CreateRelay();
    }

    private void FixedUpdate()
    {
        if(_started) return;

        if(NetworkManager.Singleton.ConnectedClients.Count >1)
        {
            OnTwoPlayerEntered?.Invoke();
            _started = true;
        }
    }

    public async void CreateRelay()
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(2);
            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            Debug.Log($"Relay Join Code: {joinCode}");

            // NGO에 Relay 데이터 설정
            var relayData = new RelayServerData(allocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayData);
            _text.text = joinCode;
            // NGO 호스트 시작
            NetworkManager.Singleton.StartHost();
        }
        catch (RelayServiceException e)
        {
            Debug.LogError($"Relay 생성 실패: {e}");
        }
    }

    public void JoinWithInputField()
    {
        JoinRelayHandle(_input.text);
    }

    public void JoinRelayHandle(string joingCOde)
    {
        JoinRelay(joinCode: joingCOde);
    }

    public async void JoinRelay(string joinCode)
    {
        try
        {
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            var relayData = new RelayServerData(joinAllocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayData);

            // NGO 클라이언트 시작
            NetworkManager.Singleton.StartClient();
        }
        catch (RelayServiceException e)
        {
            Debug.LogError($"Relay 연결 실패: {e}");
        }
    }
    

}
