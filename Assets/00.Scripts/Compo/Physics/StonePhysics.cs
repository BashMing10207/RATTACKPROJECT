using UnityEngine;

public class StonePhysics : MonoBehaviour, IGetCompoable
{
    private Rigidbody _rigidCompo;
    public void Initialize(GetCompoParent entity)
    {
        _rigidCompo = entity.GetComponentInChildren<Rigidbody>();
    }

    public void AddForce(Vector3 dir)
    {
        _rigidCompo.AddForce(dir,ForceMode.Impulse);
    }
    
    public void AddForceAt(Vector3 hitpoint, Vector3 dir)
    {
        _rigidCompo.AddForceAtPosition(dir, hitpoint, ForceMode.Impulse);
    }

}
