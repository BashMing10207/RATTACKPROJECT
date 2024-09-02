using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    static bool _isFirst = true;//ù������ �濡 ����?

    public bool isBlack;

    public NetworkVariable<int> reaminingActionCount = 
        new NetworkVariable<int>(readPerm: NetworkVariableReadPermission.Everyone, writePerm: NetworkVariableWritePermission.Owner);

    public NetworkVariable<int> maxActionCount =
    new NetworkVariable<int>(readPerm: NetworkVariableReadPermission.Everyone, writePerm: NetworkVariableWritePermission.Owner);

    //�ൿ��
    public NetworkVariable<int> reminingStone =
        new NetworkVariable<int>(readPerm: NetworkVariableReadPermission.Everyone, writePerm: NetworkVariableWritePermission.Owner);
    //���� �� ��.
    private void Awake() //�����ǰ� �ѹ���
    {
        if (_isFirst)
        {
            _isFirst = false;
            isBlack = true;
            //ù�����Τ� �� ����
        }
        else
        {
            isBlack = false;
        }
        
    }
    private void Start() //Ȱ��ȭ-��Ȱ��ȭ ��� �ѹ���
    {
        
    }
}
