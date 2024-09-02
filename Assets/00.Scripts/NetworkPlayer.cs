using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    static bool _isFirst = true;//첫번쨰로 방에 입장?

    public bool isBlack;

    public NetworkVariable<int> reaminingActionCount = 
        new NetworkVariable<int>(readPerm: NetworkVariableReadPermission.Everyone, writePerm: NetworkVariableWritePermission.Owner);

    public NetworkVariable<int> maxActionCount =
    new NetworkVariable<int>(readPerm: NetworkVariableReadPermission.Everyone, writePerm: NetworkVariableWritePermission.Owner);

    //행동력
    public NetworkVariable<int> reminingStone =
        new NetworkVariable<int>(readPerm: NetworkVariableReadPermission.Everyone, writePerm: NetworkVariableWritePermission.Owner);
    //남은 돌 수.
    private void Awake() //생성되고 한번만
    {
        if (_isFirst)
        {
            _isFirst = false;
            isBlack = true;
            //첫번쨰로ㅗ 방 입장
        }
        else
        {
            isBlack = false;
        }
        
    }
    private void Start() //활성화-비활성화 등마다 한번씩
    {
        
    }
}
