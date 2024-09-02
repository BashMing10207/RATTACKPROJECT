using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ming/Projectile")]
public class ProjectileSO : ScriptableObject
{
    public float speed, damage,power,radius,down;
    public GameObject gameObj,dieEffect;
    
}
