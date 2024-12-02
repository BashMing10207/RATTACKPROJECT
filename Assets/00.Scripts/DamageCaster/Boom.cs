using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Boom : MonoBehaviour
{
    [SerializeField]
    LayerMask _layerMask;
    [SerializeField]
    float size = 5f,damage=1,power = 1000;
    Collider[] colliders = new Collider[35];
    List<Rigidbody> _rigidbodies = new List<Rigidbody>();
    private void OnEnable()
    {
        this.GetComponent<NetworkObject>().Spawn();
        if(Physics.OverlapSphereNonAlloc(transform.position, size,colliders,_layerMask) > 0)
        {
            print("mmdsf");
        for(int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != null)
                if (colliders[i].CompareTag("Hitable"))
                {

                }
            }

        }
    Destroy(gameObject,4f);
    }
}
