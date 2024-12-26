using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : AgentManager
{

    //[SerializeField]
    public List<ItemSO> Items = new List<ItemSO>();
    //private
    public PlayerInputSO PlayerInput;
    [SerializeField]
    private float _holdMulti = 25;//�̰� ���߿� ��ǲ���� �ŷ� �E ��.

    public event Action<Unit> OnSwapUnit;
    public Vector2 PostMousePos { get; private set; } = new Vector2(0, 0);

    public bool IsHolding = false;

    [SerializeField]
    private ActSO _currentAct;
    [SerializeField]
    private ActSO _defaultAct;

    public float Upward { get; private set; } = 0;

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    protected void Init()
    {
        PlayerInput.UnitSwapEvent += SwapNextUnit;
        PlayerInput.OnClickEnter += HoldStart;
        PlayerInput.OnClickExit += HoldEnd;
        PlayerInput.OnMouseScroll += Updown;
        PlayerInput.OnClickEnter2 += HoldCancle;

        SwapUnit(0);
    }

    private void SwapNextUnit(int idx)
    {
        SwapUnit((SelectedUnitIdx + idx) % Units.Count);
    }
    private void SwapUnit(int idx)
    {
        _currentAct = _defaultAct;
        Units[SelectedUnitIdx].SelectVisual(false);
        SelectedUnitIdx = idx;
        Units[idx].SelectVisual(true);
        OnSwapUnit?.Invoke(Units[idx]);
    }

    private void HoldStart()
    {
        PostMousePos = PlayerInput.MousePos;
        IsHolding = true;
        Upward = 0;
    }

    private void Updown(float axis)
    {
        Upward += axis * Time.deltaTime * 5;
    }
        
    private void HoldEnd()
    {
        if (IsHolding)
        {
            Vector3 dir = BashUtils.V2ToV3(PostMousePos - PlayerInput.MousePos) / Screen.width * _holdMulti;
            GetAction((dir + new Vector3(0, -Upward, 0)).normalized * dir.magnitude, _currentAct);
        }
        IsHolding = false;
    }

    private void HoldCancle()
    {
        IsHolding = false;
    }

    protected override void GetAction(Vector3 dir, ActSO act)
    {
        //if(GameManager.Instance.IsPlayerturn)
        Vector3 rot = GetCompo<CameraManager>().MainCamera1.transform.eulerAngles; //��ȣ����. �� �κ��� ����� ��ũ��Ʈ�� ��κ��� ����� �ٸ� ��ũ��Ʈ�� �ű� ��.
        dir = Quaternion.Euler(0,rot.y,0) * dir;

        base.GetAction(dir, act);

        GetCompo<SkillAnimator>().SetAnim(act.HashValue);
        GetCompo<PlayerActions>().AttackAnim();//��ȣ������
    }
    public void SetAct(ActSO act)
    {
        _currentAct = act;

        ////�� �Ʒ��� ���Ŀ� �ٸ� ��ũ��Ʈ�� ����(�� �����ϴ�...)
        //GetCompo<PlayerActions>().
    }
}

