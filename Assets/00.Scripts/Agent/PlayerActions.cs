using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerActions : MonoBehaviour, IGetCompoable
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

    private PlayerManager _parent;

    private int _currentActType = 0, _currentActIdx = 0;
    public void Initialize(GetCompoParent entity)
    {
        _parent = entity as PlayerManager;

        for (int i = 0; i < _cardParents.Count; i++)
        {
            _cards.Add(_cardParents[i].GetComponentsInChildren<UICards>().ToList());
        }
        _parent.PlayerInput.Arrow += Arrow;
    }

    private void Start()
    {
        Init();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.IsPlayerturn)
        {
            SetTrm();
            if (_parent.IsHolding)
            {
                
            }
        }
    }

    private void SetTrm()
    {
        Vector3 rot = _parent.GetCompo<CameraManager>().MainCamera1.transform.eulerAngles;
        Vector3 dir = Quaternion.Euler(0, rot.y, 0) * (BashUtils.V2ToV3(_parent.PostMousePos-Mouse.current.position.value).normalized);
        //_skillAnimator.transform.SetPositionAndRotation(_parent.SelectedUnit().WeaponTrm.position ,Quaternion.FromToRotation(Vector3.forward, dir));
        _skillAnimator.transform.position = _parent.SelectedUnit().WeaponTrm.position;
        _skillAnimator.transform.rotation = Quaternion.Lerp(_skillAnimator.transform.rotation, Quaternion.FromToRotation(Vector3.forward, dir), 0.2f);
    }

    private void Arrow(Vector2 dir)
    {
        SetAction(((int)dir.y + _currentActIdx)%2, ((int)dir.x + _currentActIdx) % (((int)dir.y + _currentActIdx) % 2 ==0 ? 
            _parent.SelectedUnit().GetCompo<AgentActCommander>().OwnActs.Count: _parent.Items.Count));
    }



    public void SetAction(int type, int idx)
    {
        _skillAnimator.SetAnim(_cards[type][idx].Act.HashValue);
        _cards[_currentActType][_currentActIdx].OutLineHandle(false);

        _currentActType = type;
        _currentActIdx = idx;

        _parent.SetAct(_cards[type][idx].Act);
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

        List<ActSO> unitOwnList = _parent.SelectedUnit().GetCompo<AgentActCommander>().OwnActs;

        for (int j = 0; j < _cards[0].Count; j++)
        {
            bool isExist = unitOwnList.Count > j;
            _cards[0][j].SetActive(isExist);

            if (isExist)
                _cards[0][j].Init(unitOwnList[j],j,0);

        }

        for (int j = 0; j < _cards[0].Count; j++)
        {
            bool isExist = _parent.Items.Count > j;
            _cards[1][j].SetActive(isExist);

            if (isExist)
                _cards[1][j].Init(_parent.Items[j].Act,j,1);

        }
    }

    public void SetToolTip(Vector3 pos, string s)
    {
        _toolTip.Init(pos, s);
    }
    public void DisableToolTip()
    {
        _toolTip.Disable();
    }

    public void AttackAnim()
    {
        _skillAnimator.SetAnim("Attack");
        Init();
    }
}
