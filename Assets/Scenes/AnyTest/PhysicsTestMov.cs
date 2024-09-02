using UnityEngine;

public class PhysicsTestMov : MonoBehaviour
{
    [SerializeField]
    float _speed,_downPower;
    float _tmpDownPower;
    [SerializeField]
    LayerMask _layerMask;
    Vector3 _upVector,_dir;
    public void Start()
    {
        _dir = transform.up*_speed;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitomi = Physics2D.Raycast(transform.position, _dir, _dir.magnitude * Time.deltaTime, _layerMask);

        if (hitomi)
        {
            if(hitomi.transform.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
                rb.AddForce(new Vector2(0,9999f));

            Destroy(gameObject);
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        _dir += (Vector3)(mousePos-(Vector2)transform.position) * _downPower * Time.deltaTime;
      

        transform.position += (_dir*Time.deltaTime);

    }

}
