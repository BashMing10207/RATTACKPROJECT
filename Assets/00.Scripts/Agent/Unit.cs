using UnityEngine;

public class Unit : Agent
{

    public bool IsSelected = false;
    [SerializeField] private GameObject _isSelectedObj, _isDisabledObj;
    public Transform ViewPivot;
    public Transform WeaponTrm;
    public GetCompoParent MasterController;

    public void Init(GetCompoParent masterController)
    {
        MasterController = masterController;
    }

    public void SelectVisual(bool enable)
    {
        IsSelected = enable;
        if(_isSelectedObj != null)
        _isSelectedObj.SetActive(enable);
        if(_isDisabledObj != null)
        _isDisabledObj.SetActive(!enable);
    }
}

