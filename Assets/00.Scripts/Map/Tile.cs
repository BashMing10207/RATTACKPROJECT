using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private TileType _tileType;
    [SerializeField]
    private MeshRenderer _meshRenderer;
    [SerializeField]
    private MeshFilter _meshfilter;

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
}
