using UnityEngine;

public enum ActType
    {
        Projecile,
        Passive,
        Active
    }
//[CreateAssetMenu(fileName="SO/Act")]
public abstract class ActSO : ScriptableObject
{
    
    public float MinCost = 1f, MaxCost = 10f;

    public string ActName = "";
    [Multiline]
    public string Description = "";

    public Sprite Icon;

    public string AnimParamName;
    public int HashValue;

    private void OnValidate()
    {
        HashValue = Animator.StringToHash(AnimParamName);
    }

    public abstract void RunAct(Vector3 dir, ref Agent agent);
}
