using System;
using UnityEngine;

public enum ActType
    {
        Projectile,
        Impact,
        Melee,
        Move,
        Buff,
        Passive
    }
//[CreateAssetMenu(fileName="SO/Act")]
[Serializable]
public abstract class ActSO : ScriptableObject
{
    
    public float MinCost = 1f, MaxCost = 10f;

    public string ActName = "";
    [Multiline]
    public string Description = "";

    public Sprite Icon;

    public string AnimParamName;
    public int HashValue;


    public ActType ActTypeEnum;
    public float MaxDistance = 10f;

    public int SKillCoollDown = 1;
    private void OnValidate()
    {
        HashValue = Animator.StringToHash(AnimParamName);
    }

    public abstract void RunAct(Vector3 dir,Agent agent);
}
