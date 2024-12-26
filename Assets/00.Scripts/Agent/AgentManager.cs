using System;
using System.Collections.Generic;
using UnityEngine;

    public class AgentManager : GetCompoParent // : Manager<AgentManager>
    {
        public List<Unit> Units = new List<Unit>();

        public int SelectedUnitIdx = 0;

        public event Action<Vector3,ActSO> ActionExcutor;
        public Unit SelectedUnit() => Units[SelectedUnitIdx];
        protected virtual void GetAction(Vector3 dir,ActSO act)
        {
            ActionExcutor?.Invoke(dir,act);
        }
    }

