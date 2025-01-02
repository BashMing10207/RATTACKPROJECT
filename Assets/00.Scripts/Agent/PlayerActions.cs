using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerActions : MonoBehaviour, IGetCompoable,IAfterInitable
{
    [SerializeField]
    private ToolTip _toolTip;
    private List<List<UICards>> _cards = new List<List<UICards>>();

    [SerializeField]
    private List<RefreshableLayout> _cardParents = new List<RefreshableLayout>();
    [SerializeField]
    private SkillAnimator _skillAnimator;
    //[SerializeField]
    //private LayoutGroup _layoutGroup;

    private Player _parent;

    private PlayerAgentManager _agentManager;
    private ItemManager _itemManager;
    [SerializeField]
    private int _currentActType = 0, _currentActIdx = 0;

    private bool _isEnabled = true;

    private void Start()
    {
        GameManager.Instance.OnTurnEndEvent +=Init;
    }

    public bool IsOnPointer { get; private set; } = false;
    public void Initialize(GetCompoParent entity)
    {
        _parent = entity as Player;

        for (int i = 0; i < _cardParents.Count; i++)
        {
            _cards.Add(_cardParents[i].GetComponentsInChildren<UICards>().ToList());
        }
        GameManager.Instance.PlayerInputSO.Arrow += Arrow;

       
    }

    public void AfterInit()
    {
        _agentManager = _parent.GetCompo<PlayerAgentManager>();
        _itemManager = _parent.GetCompo<ItemManager>();
    }

    private void OnDisable()
    {
        _isEnabled = true;
    }

    private void Update()
    {
        if (_isEnabled)
        {
            _isEnabled = false;
            Init();
        }
    }

    private void FixedUpdate()
    {

        if (GameManager.Instance.IsPlayerturn)
        {
            SetTrm();
            if (_agentManager.IsHolding)
            {

            }
        }
    }

    private void SetTrm()
    {
        Vector3 rot = _parent.GetCompo<CameraManager>().MainCamera1.transform.eulerAngles;
        Vector3 dir = Quaternion.Euler(0, rot.y, 0) * (BashUtils.V2ToV3(_agentManager.PostMousePos - Mouse.current.position.value).normalized);
        //_skillAnimator.transform.SetPositionAndRotation(_parent.SelectedUnit().WeaponTrm.position ,Quaternion.FromToRotation(Vector3.forward, dir));
        _skillAnimator.transform.position = _agentManager.SelectedUnit().WeaponTrm.position;
        _skillAnimator.transform.rotation = Quaternion.Lerp(_skillAnimator.transform.rotation, Quaternion.FromToRotation(Vector3.forward, dir), 0.45f);
    }

    private void Arrow(Vector2 dir)
    {
        SetAction(((int)dir.y + _currentActIdx) % 2, ((int)dir.x + _currentActIdx) % (((int)dir.y + _currentActIdx) % 2 == 0 ?
            _agentManager.SelectedUnit().GetCompo<AgentActCommander>().OwnActs.Count : _itemManager.Items.Count));
    }

    public void SetCurrentAct()
    {
        SetAction(_currentActType, _currentActIdx);
    }

    public void SetAction(int type, int idx)
    {
        _skillAnimator.SetAnim(_cards[type][idx].Act.HashValue);
        _cards[_currentActType][_currentActIdx].OutLineHandle(false);

        //_currentActType = type;
        Debug.Log("이부분은 나중에 알피지 할 때 쓰이다");
        _currentActIdx = idx;

        _parent.GetCompo<ActCommander>(true).SetAct(_cards[type][idx].Act);
        _cards[type][idx].OutLineHandle(true);

        _cardParents[0].Refresh();
        _cardParents[1].Refresh();
    }

    public void Init()
    {
        //for (int i = 0; i < _cards.Count; i++)
        //{
        //    for (int j = 0; j < _cards[i].Count; j++)
        //    {
        //        _cards[i][j].Init()
        //    }
        //}

        List<ActSO> unitOwnList = _agentManager.SelectedUnit().GetCompo<AgentActCommander>().OwnActs;

        for (int j = 0; j < _cards[0].Count; j++)
        {
            bool isExist = unitOwnList.Count > j;
            _cards[0][j].SetActive(isExist);

            if (isExist)
                _cards[0][j].Init(unitOwnList[j], j, 0);

        }

        for (int j = 0; j < _cards[0].Count; j++)
        {
            bool isExist = _itemManager.Items.Count > j;
            _cards[1][j].SetActive(isExist);

            if (isExist)
                _cards[1][j].Init(_itemManager.Items[j], j, 1);

        }

        SetAction(_currentActType, _currentActIdx);
    }

    public void SetToolTip(Vector3 pos, string s)
    {
        _toolTip.Init(pos, s);
        IsOnPointer = true;
    }
    public void DisableToolTip()
    {
        _toolTip.Disable();
        IsOnPointer = false;
    }

    public void AttackAnim()
    {
        _skillAnimator.SetAnim("Attack");

        if (_itemManager.Items.Count > 0)
        {
            if (_currentActType == 1)
            {
                _itemManager.Items.RemoveAt(_currentActIdx);
            }
            List<ActSO> unitOwnList = _agentManager.SelectedUnit().GetCompo<AgentActCommander>().OwnActs;

            for (int j = 0; j < _cards[0].Count; j++)
            {
                bool isExist = unitOwnList.Count > j;
                _cards[0][j].SetActive(isExist);

                if (isExist)
                    _cards[0][j].Init(unitOwnList[j], j, 0);

            }

            for (int j = 0; j < _cards[0].Count; j++)
            {
                bool isExist = _itemManager.Items.Count > j;
                _cards[1][j].SetActive(isExist);

                if (isExist)
                    _cards[1][j].Init(_itemManager.Items[j], j, 1);

            }
        }

        StartCoroutine(RemoveCards());
    }

    private IEnumerator RemoveCards()
    {
        yield return new WaitForSecondsRealtime(0.05f);
        //if (_parent.Items.Count > 0)
        //{
        //    if (_currentActType == 1)
        //    {
        //        _parent.Items.RemoveAt(_currentActIdx);
        //    }
        //    Init();
        //}

        SetAction(_currentActType, _currentActIdx);
        if (_itemManager.Items.Count <= 0)
        {
            _parent.GetCompo<ActCommander>(true).CurrentAct = null;

        }
    }


}
