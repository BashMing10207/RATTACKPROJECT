using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitSkillInitSO", menuName = "SO/UnitSkillInitSO")]
public class UnitSkillInitSO : ScriptableObject
{
    public List<ActSO> CanHaveSkillList = new();
}
