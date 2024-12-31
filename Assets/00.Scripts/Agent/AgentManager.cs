using System;
using System.Collections.Generic;
using UnityEngine;

    public class AgentManager : MonoBehaviour,IGetCompoable//GetCompoParent // : Manager<AgentManager>
    {
        public List<Unit> Units = new List<Unit>();

        public int SelectedUnitIdx = 0;

        public event Action<Vector3> ActionExcutor;

    protected GetCompoParent _parent;

    public virtual void Initialize(GetCompoParent entity)
    {
        _parent = entity;
    }

    public Unit SelectedUnit() => Units[SelectedUnitIdx];
        protected virtual void GetAction(Vector3 dir)
        {
            ActionExcutor?.Invoke(dir);
        }
    }

