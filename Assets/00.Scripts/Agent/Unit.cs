using UnityEngine;

    public class Unit : Agent
    {
        [SerializeField] private GameObject _isSelectedObj,_isDisabledObj;
        public void SelectVisual(bool enable)
        {
            _isSelectedObj.SetActive(enable);
            _isDisabledObj.SetActive(!enable);
        }
    }

