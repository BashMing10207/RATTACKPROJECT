
using System.Collections.Generic;
using UnityEngine;

public class SkillAdder : MonoBehaviour, IGetCompoable
{
    private GetCompoParent _playerManager;
    public List<ActSO> AddSkillList = new List<ActSO>();

    private void Start()
    {
        GameManager.Instance.OnTwoTurnEndEvent += AddSkill;
    }


    public void Initialize(GetCompoParent entity)
    {
        _playerManager= entity;
    }

    public void AddAddList(ActSO act)
    {
        AddSkillList.Add(act);
    }

    public void RemoveAddList(ActSO act)
    {
        AddSkillList.Remove(act);
    }

    public void AddSkill()
    {
        _playerManager.GetCompo<ItemManager>(true).Items.AddRange(AddSkillList);
    }
}
