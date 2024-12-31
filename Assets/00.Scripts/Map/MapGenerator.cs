using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//R:Height
//G:rotate
//B:--¤¡
//  0~ :dirt
//  50~:grass
//  100~:stone
//  150~:Ice
//  200~:Lava
//  250~:Null
//A:

public enum TileType
{
    Dirt,
    Grass,
    Stone,
    Ice,
    Lava,
    Snow
}

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator Instance;

    [SerializeField]
    Texture2D _test;
    [SerializeField]
    int _size=50;
    [SerializeField]
    float _tileSize = 2,_height=225,_speed=100;
    [SerializeField]
    public List<GroundSO>tileTypeSO=new List<GroundSO>();

    public Tile[,] tiles = new Tile[64,64];
    public Vector3[,] targetPoses = new Vector3[64,64];
    
    [SerializeField]
    GameObject _pref;
    [SerializeField]
    Texture2D[] mapImgs;
    int mapIdx = 0;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        //var bArray1 = _test.GetRawTextureData<Color32>();
        var bArray = _test.GetPixels32(0);
        //string texAsString = Convert.ToBase64String(bArray);
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                tiles[i,j] = Instantiate(_pref,transform.position +  new Vector3(i * _tileSize,((float)(bArray[(_size) * i + j]).r)/_height , j * _tileSize), Quaternion.identity,transform)
                    .GetComponent<Tile>();
                targetPoses[i, j] = transform.position + new Vector3(i * _tileSize, ((float)(bArray[(_size) * i + j]).r) / _height, j * _tileSize);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                tiles[i, j].transform.position = Vector3.Lerp(tiles[i, j].transform.position, targetPoses[i, j], _speed * Time.deltaTime);
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            //ChangeMap(_test);
            ming();
        }
    }
    [ContextMenu("asdfdfsa")]
    public void ming()
    {
        mapIdx = ++mapIdx % mapImgs.Length;
        ChangeMap(mapImgs[mapIdx]);
    }
    public void ChangeMap(Texture2D targetmap)
    {
        //var bArray1 = _test.GetRawTextureData<Color32>();
        var bArray = targetmap.GetPixels32(0);
        //string texAsString = Convert.ToBase64String(bArray);
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                targetPoses[i, j] = transform.position + new Vector3(i * _tileSize, ((float)(bArray[(_size) * i + j]).r) / _height, j * _tileSize);
                int a = (int)((float)(bArray[(_size) * i + j]).b)/50;
                tiles[i,j].ChangeTile(TileType.Dirt+a);
                //tiles[i, j].material = _tileMats[];
            }
        }
    }
}
