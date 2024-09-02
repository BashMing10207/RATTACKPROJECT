using Cinemachine;
using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public enum ActivedSkill
{
    move,
    create,
    fireball,
    arrow,
    throwBox
};
public class NetPlayerMana : NetworkBehaviour
{
    public static NetworkVariable<bool> isBlackTurn = new NetworkVariable<bool>(value: true);
    public static NetworkVariable<int> currentNum = new NetworkVariable<int>(value: 0,writePerm: NetworkVariableWritePermission.Server);
    public static NetPlayerStone GetCurrentStone
    {
        get
        {
            return stones[isBlackTurn.Value ? 0 : 1][currentNum.Value];
        }
    }
    public static List<NetPlayerStone>[] stones = new List<NetPlayerStone>[2] { new List<NetPlayerStone>(), new List<NetPlayerStone>() };
    public static NetworkVariable <int>[] extraLifeCount = new NetworkVariable<int>[2] {new NetworkVariable<int>(value:6), new NetworkVariable<int>(value: 6) };
    public static event Action OnTurnEnd;
    public CinemachineVirtualCamera vCamera;
    public Camera mainCam;
    public bool isActionSelected = false;

    public Transform[] StonePrefs;

    //public ProjectileSO fireball;//임시 테스트용
    public static ProjectileSO ProjectileToShoot { get; set; }
    ActivedSkill activedSkill;

    public int extraLife = 3;

    #region mouseForceMove
    Vector3 tempMousePos;
    public LineRenderer lineRenderer;
    #endregion

    private GameObject playerHand;

    public int actioncount=0;
    void Awake()
    {
        
        NetControlUI.INSTANCE.OnJoin(TestLobby.Code);
        vCamera = NetGameMana.Instance.GetComponentInChildren<CinemachineVirtualCamera>();
        //if (NetGameMana.INSTANCE.player != null)
        //{
        //    Destroy(vCamera);
        //    Destroy(Camera.main.GetComponent<CinemachineBrain>());
        //}
        mainCam = Camera.main;
  
      
        
        lineRenderer = mainCam.transform.root.GetComponentInChildren<LineRenderer>();

      
    }

    private void Start()
    {
        if(IsOwner)
        {
            NetGameMana.Instance.player = this;
            //NetGameMana.Instance.playerHand.GetComponent<PlayerHand>().playerInventory = GetComponent<PlayerInventory>();
            //NetGameMana.Instance.playerHand.GetComponent<PlayerHand>().StartCreateCard();
            WasdServerRpc();
        }
        
        playerHand = NetGameMana.Instance.playerHand;
        if(IsHost)
        {
            
        }
    }
    [ServerRpc]
    void WasdServerRpc()
    {
        
    }
    void Update()
    {
        if (!IsOwner)
            return;

        if (stones[isBlackTurn.Value ? 0 : 1].Count > 0)
        {
            vCamera.LookAt = stones[isBlackTurn.Value ? 0 : 1][currentNum.Value].pivot;
            vCamera.Follow = stones[isBlackTurn.Value ? 0 : 1][currentNum.Value].pivot;
        }
        NetworkUpdate();
    }

    private void FixedUpdate()
    {
        if (!IsOwner)
        {
            return;
        }

        //NetGameMana.Instance.lifeUI.ChangeLife();
    }

    void NetworkUpdate()
    {
        if (!(IsHost ^ isBlackTurn.Value))
        {
            BaseAction();//숙청
            PlayerActionMing();

            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.LeftShift))
            {
                EndTurnServerRpc();
            }
        }
    }

    void BaseAction()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            activedSkill = ActivedSkill.move;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            activedSkill = ActivedSkill.create;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            activedSkill = ActivedSkill.fireball;
        }


        if (Input.GetKeyDown(KeyCode.Tab))
        {
            CamChangeServerRpc();
        }
    }

    void CamChange()
    {
        DisAbleOutline();
        if (stones[isBlackTurn.Value ? 0 : 1].Count > 0)
        {
            //vCamera.LookAt = stones[isHostTurn.Value ? 0 : 1][currentNum.Value].pivot;
            //vCamera.Follow = stones[isHostTurn.Value ? 0 : 1][currentNum.Value].pivot;
        }
        SetOutline(true);
        //카메라 팔로우-룩앳 바꾸기
    }
    [ServerRpc]
    void EndTurnServerRpc()
    {
        EndTurnClientRpc();
        isBlackTurn.Value = !isBlackTurn.Value;
        currentNum.Value = 0;
        CamChangeServerRpc();
    }
    [ClientRpc]
    void EndTurnClientRpc()
    {
        OnTurnEnd?.Invoke();
        ProjectileToShoot = null;
    }
    [ClientRpc]
    public void CamChangeClientRpc()
    {
        CamChange();
    }
    [ServerRpc]
    public void CamChangeServerRpc()
    {
        currentNum.Value = (currentNum.Value + 1) % stones[isBlackTurn.Value ? 0 : 1].Count;
        CamChangeClientRpc();
    }
    [ServerRpc]
    void AddExtraLifeServerRpc(int index,int num)//돌 생성 시 index는 0:검돌1:흰돌, num은 -1로 호출해야뒤~
    {
        extraLifeCount[index].Value+=num;
    }

    void PlayerActionMing()
    {
        
        if (IsOwner)
        {
            if(stones[isBlackTurn.Value ? 0 : 1].Count > 0)
            {

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                tempMousePos = Input.mousePosition;
                lineRenderer.enabled = true;
                Vector3 a = mainCam.WorldToScreenPoint(stones[isBlackTurn.Value ? 0 : 1][currentNum.Value].transform.position);
                lineRenderer.SetPosition(0, a + Vector3.back * a.z);
            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                Vector3 mousepos = (Input.mousePosition - tempMousePos);
                float distance = Mathf.Clamp(mousepos.magnitude, 0, 1000);

                Vector3 a = mainCam.WorldToScreenPoint(stones[isBlackTurn.Value ? 0 : 1][currentNum.Value].transform.position);
                lineRenderer.SetPosition(1, mousepos.normalized * distance + a + Vector3.back * a.z);
            }
            }


            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                Vector3 forceInput = (Input.mousePosition - tempMousePos);
                float magnitude = forceInput.magnitude;
                magnitude = Mathf.Clamp(magnitude, 0, 1000);

                RaycastHit hit;
                if (Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out hit))
                {
                }
                    Vector3 mousepos = hit.point;
                    WhatActionServerRpc(mousepos, forceInput, magnitude, activedSkill);
                //print(forceInput.normalized);
                lineRenderer.enabled = false;
                actioncount++;
                if (actioncount > 1)
                {
                    actioncount = 0;
                    EndTurnServerRpc();
                }
            }
        }

    }
    [ServerRpc]
    void WhatActionServerRpc(Vector3 inputpos,Vector3 forceInput, float magnitude,ActivedSkill whatSkill)
    {
        switch ((whatSkill))
        {
            case ActivedSkill.move:
                stones[isBlackTurn.Value ? 0 : 1][currentNum.Value].ForceMove(new Vector3(forceInput.x, 0, forceInput.y).normalized, -magnitude, 1);
                NetGameMana.Instance.AtSo.Play();
                break;
                
            case ActivedSkill.create:

                if (extraLifeCount[isBlackTurn.Value ? 0 : 1].Value > 0)
                {
                inputpos = new Vector3(inputpos.x, 10, inputpos.z);
                Transform spawnedObj = Instantiate(StonePrefs[isBlackTurn.Value ? 0 : 1], inputpos, Quaternion.identity);
                spawnedObj.GetComponent<NetworkObject>().Spawn(true);
                    extraLifeCount[isBlackTurn.Value ? 0 : 1].Value--;
                //AddExtraLifeServerRpc(isHostTurn.Value ? 0 : 1, -1);
                }

                break;
            case ActivedSkill.fireball:

                //NetGameMana.INSTANCE.pool.GiveServerRpc(fireball, transform).GetComponent<Projectile>()
                print("n3");
                if(ProjectileToShoot != null)
                {
                    print("prok is not null");
                    GameObject projectile1 = Instantiate(ProjectileToShoot.gameObj);
                    projectile1.GetComponent<Projectile>()
                        .Init(new Vector3(forceInput.x, 0, forceInput.y).normalized + Vector3.up * 0.5f, stones[isBlackTurn.Value ? 0 : 1][currentNum.Value].transform.position + Vector3.up * 1.5f,
                        magnitude / 600);
                    projectile1.GetComponent<NetworkObject>().Spawn(true);
                }
                else
                {
                    print("proj is null");
                }

                //GameObject projectile1 = Instantiate(fireball.gameObj);
                //projectile1.GetComponent<Projectile>()
                //    .Init(new Vector3(forceInput.x, 0, forceInput.y).normalized + Vector3.up * 0.5f, stones[isHostTurn.Value ? 0 : 1][currentNum.Value].transform.position + Vector3.up * 1.5f,
                //    magnitude / 600);
                //projectile1.GetComponent<NetworkObject>().Spawn(true);

                break;
            case ActivedSkill.arrow:
                break;
            case ActivedSkill.throwBox:
                break;
        };
    }

    private void SetOutline(bool active)
    {
        NetPlayerStone chooseStone = stones[isBlackTurn.Value ? 0 : 1][currentNum.Value];
        chooseStone.outLine.SetActive(active);
    }
    private void DisAbleOutline()
    {
        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < stones[j].Count; i++)
            {
                stones[j][i].outLine.SetActive(false);
            }
        }
    }
}
