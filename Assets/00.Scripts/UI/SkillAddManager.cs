
using System.Collections.Generic;
using UnityEngine;

public class SkillAddManager : MonoBehaviour, IGetCompoable
{
    private GameManager _getCompoParent;

    [SerializeField]
    private List<SkillSelectUI> _skillCards = new List<SkillSelectUI>();

    [SerializeField]
    private List<ActSO> _acts = new List<ActSO>();
    public void Initialize(GetCompoParent entity)
    {
        _getCompoParent = entity as GameManager;
    }

    public void AddAction(ActSO act)
    {
        _getCompoParent.PlayerManagerCompo.GetCompo<SkillAdder>().AddAddList(act);
        _getCompoParent.PlayerManagerCompo.GetCompo<PlayerActions>().Init();
    }

    private void OnEnable()
    {
        Init();  
    }
    public void Init()
    {
        for (int i = 0; i < _skillCards.Count; i++)
        {
            _skillCards[i].Init(_acts[Random.Range(0, _acts.Count)], i, 1);
        }
    }
}
