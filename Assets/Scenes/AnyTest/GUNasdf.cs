using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUNasdf : MonoBehaviour
{
    [SerializeField]
    GameObject _bulletPref;
    [SerializeField]
    Transform _shootingPoint;
    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.rotation = Quaternion.LookRotation(Vector3.forward,mousePos-new Vector2(transform.position.x,transform.position.y));

        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_bulletPref, _shootingPoint.position, _shootingPoint.rotation);
            
        }
    }
}
