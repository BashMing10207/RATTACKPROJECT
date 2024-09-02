using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileType TileType;

    public MeshRenderer meshRenderer;
    public MeshFilter meshfilter;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    public void ChangeTile(TileType type)
    {
        meshfilter.mesh = MapMover.Instance.tileTypeSO[(int)type].mesh;
        meshRenderer.material = MapMover.Instance.tileTypeSO[(int)type].material;
        print(type.ToString());
    }
}
