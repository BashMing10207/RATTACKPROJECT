
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour, IGetCompoable
{
    protected GetCompoParent _parent;

    public List<ActSO> Items = new();

    public void Initialize(GetCompoParent entity)
    {
        _parent = entity;
    }


}
