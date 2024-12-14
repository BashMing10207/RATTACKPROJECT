using UnityEngine;

    public class PlayerManager : AgentManager
    {
       
        [SerializeField]
        private PlayerInputSO _playerInput;

        private Vector2 _postMousePos=new Vector2(0,0);

        private bool _isHolding = false;

    [SerializeField]
    private ActSO _currentAct;
    [SerializeField]
    private ActSO _defaultAct;

        protected override void Awake()
        {
            base.Awake();
            Init();
        }

        protected void Init()
        {
            _playerInput.UnitSwapEvent += SwapNextUnit;
            _playerInput.OnClickEnter += HoldStart;
            _playerInput.OnClickExit += HoldEnd;
        }

        private void SwapNextUnit(int idx)
        {
            SwapUnit((SelectedUnitIdx + idx) % Units.Count);
        }
        private void SwapUnit(int idx)
        {
            _currentAct = _defaultAct;
            Units[idx].SelectVisual(false);
            SelectedUnitIdx = idx;
            Units[idx].SelectVisual(true);
        }

        private void HoldStart()
        {
            _postMousePos = _playerInput.MousePos;
            _isHolding=true;
        }
        private void HoldEnd()
        {
            _isHolding=false;
          GetAction( BashUtils.V2ToV3(_postMousePos - _playerInput.MousePos),_currentAct);
        }

    protected override void GetAction(Vector3 dir, ActSO act)
    {
        //if(GameManager.Instance.IsPlayerturn)
        base.GetAction(dir, act);
    }
    public void SetAct(ActSO act)
    {
        _currentAct = act;
    }
    private void Update()
    {
        if (GameManager.Instance.IsPlayerturn)
        {

        }
    }
}

