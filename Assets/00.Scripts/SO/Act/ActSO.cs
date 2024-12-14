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
    public string Description = "";


    public abstract void RunAct(Vector3 dir, ref Agent agent);
}
