using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum NewSkillType
    {
        Projecile,
        Passive,
        Active
    }
[CreateAssetMenu(fileName="SkillMing")]
public class SkillSo : ScriptableObject
{
    
    public NewSkillType skillType;
    [Header("Projectile")]
    public ProjectileSO projectile;
    //[Header("ActiveStatUp")]


}
