using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : DamageCaster
{
    [SerializeField]
    private TileType _tileType;
    [SerializeField]
    private MeshRenderer _meshRenderer;
    [SerializeField]
    private MeshFilter _meshfilter;
    private float _time = 0;


    [SerializeField]
    private Collider _collider;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    public void ChangeTile(TileType type)
    {
        _meshfilter.mesh = MapGenerator.Instance.TileTypeSO[(int)type].mesh;
        _meshRenderer.material = MapGenerator.Instance.TileTypeSO[(int)type].material;
        _tileType = type;
        //print(type.ToString());
    }

    public void DownTile(float value)
    {
        StartCoroutine(DownCorutin(value));
    }

    private IEnumerator DownCorutin(float value)
    {
        for (float i = 0; i < value; i+=0.02f)
        {
            transform.position -= new Vector3(0,0.02f,0);
            yield return new WaitForSeconds(0.02f);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (_tileType == TileType.Lava)
        {
            DamageCast(collision.gameObject, 0.1f);
        }

    }
}
