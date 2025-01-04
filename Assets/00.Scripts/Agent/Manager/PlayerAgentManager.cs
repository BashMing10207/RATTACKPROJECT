using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAgentManager : AgentManager,IGetCompoable
{
    //private

    [SerializeField]
    private float _holdMulti = 25;//�̰� ���߿� ��ǲ���� �ŷ� �E ��.

    public Vector2 PostMousePos { get; private set; } = new Vector2(0, 0);

    public bool IsHolding = false;

    [SerializeField]
    private ActSO _defaultAct;

    public UnityEvent StartHold;
    public UnityEvent EndHold;
    public UnityEvent ChangeSelectUnit;

    private PlayerInputSO _playerInput;
    public float Upward { get; private set; } = 0;



    public override void Initialize(GetCompoParent entity)
    {
        base.Initialize(entity);
        Init();
    }


    protected override void Start()
    {
        base.Start();
        //MultiGameManager.Instance.PlayerManagerCompo.Add(this);
    }

    protected void Init()
    {
        _playerInput = GameManager.Instance.PlayerInputSO;

        _playerInput.UnitSwapEvent += SwapNextUnit;
        _playerInput.OnClickEnter += HoldStart;
        _playerInput.OnClickExit += HoldEnd;
        _playerInput.OnMouseScroll += Updown;
        _playerInput.OnClickEnter2 += HoldCancle;



        SwapUnit(0);
    }

    private void SwapNextUnit(int idx)
    {
        SwapUnit(Mathf.Abs(SelectedUnitIdx + idx) % Units.Count);
    }
    protected override void SwapUnit(int idx)
    {
        base.SwapUnit(idx);
        ChangeSelectUnit?.Invoke();
    }

    private void HoldStart()
    {
        if (!_parent.GetCompo<PlayerActions>().IsOnPointer)
        {
            PostMousePos = _playerInput.MousePos;
            IsHolding = true;
            Upward = 0;
            StartHold?.Invoke();
        }
    }

    private void Updown(float axis)
    {
        Upward += axis * Time.deltaTime * 5;
    }

    private void HoldEnd()
    {

        if (IsHolding)
        {
            Vector3 dir = BashUtils.V2ToV3(PostMousePos - _playerInput.MousePos) / Screen.width * _holdMulti;
            GetAction((dir + new Vector3(0, -Upward, 0)).normalized * dir.magnitude);
        }
        IsHolding = false;

        EndHold?.Invoke();
    }

    private void HoldCancle()
    {
        IsHolding = false;
        EndHold?.Invoke();
    }

    protected override void GetAction(Vector3 dir)
    {
        if (gameObject.activeInHierarchy)
        {
            //if(GameManager.Instance.IsPlayerturn)
            Vector3 rot = _parent.GetCompo<CameraManager>().MainCamera1.transform.eulerAngles;
            dir = Quaternion.Euler(0, rot.y, 0) * dir;

            base.GetAction(dir);
        }
    }

    public override void UnitDie(Unit unit)
    {
        base.UnitDie(unit);
    }
}
