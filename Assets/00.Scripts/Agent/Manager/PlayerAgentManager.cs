using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAgentManager : AgentManager
{
    //private

    [SerializeField]
    private float _holdMulti = 25;//�̰� ���߿� ��ǲ���� �ŷ� �E ��.

    public event Action<Unit> OnSwapUnit;
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


    private void Start()
    {
        //MultiGameManager.Instance.PlayerManagerCompo.Add(this);
    }

    protected void Init()
    {
        _playerInput = (_parent as Player).PlayerInput;

        _playerInput.UnitSwapEvent += SwapNextUnit;
        _playerInput.OnClickEnter += HoldStart;
        _playerInput.OnClickExit += HoldEnd;
        _playerInput.OnMouseScroll += Updown;
        _playerInput.OnClickEnter2 += HoldCancle;

        SwapUnit(0);
    }

    private void Update()
    {
        for (int i = 0; i < Units.Count; i++)
        {
            if (Units[i].transform.position.y < 0)
            {
                Units.RemoveAt(i);
            }
        }
    }

    private void SwapNextUnit(int idx)
    {
        SwapUnit((SelectedUnitIdx + idx) % Units.Count);
    }
    private void SwapUnit(int idx)
    {
        ChangeSelectUnit?.Invoke();
        Units[SelectedUnitIdx].SelectVisual(false);
        SelectedUnitIdx = idx;
        Units[idx].SelectVisual(true);
        OnSwapUnit?.Invoke(Units[idx]);
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

}
