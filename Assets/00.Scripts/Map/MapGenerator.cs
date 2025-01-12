using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//R:Height
//G:Enemy
//1~10

//B:--¤¡
//  0~ :dirt  
//  50~:grass
//  100~:stone
//  150~:Ice
//  200~:Lava
//  250~:snow
//A:Player
//1~10


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
    private Texture2D _test;
    [SerializeField]
    private int _size = 50;
    [SerializeField]
    private float _tileSize = 2, _height = 225, _speed = 100;

    public List<GroundSO> TileTypeSO = new();

    [SerializeField]
    private float _heightOffset = 4;

    public Tile[,] Tiles = new Tile[64, 64];
    public Vector3[,] TargetPoses = new Vector3[64, 64];

    [SerializeField]
    private Tile _pref;
    [SerializeField]
    private Texture2D[] _mapImgs;
    private int _mapIdx = 0;

    [SerializeField]
    private AgentManager _playerAgentManager;

    private float _time = 0;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    private void Start()
    {

        //var bArray1 = _test.GetRawTextureData<Color32>();
        //var bArray = _test.GetPixels32(0);
        //string texAsString = Convert.ToBase64String(bArray);
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                //float heighttmp = (float)(bArray[(_size) * i + j]).r;
                Tiles[i, j] = Instantiate(_pref, transform.position + new Vector3(i * _tileSize, 0, j * _tileSize), Quaternion.identity, transform);
                TargetPoses[i, j] = transform.position + new Vector3(i * _tileSize, 0, j * _tileSize);
            }
        }
        ChangeMap(_test);
    }

    // Update is called once per frame
    private void Update()
    {

        if(_time>0)
        {
            _time -= Time.deltaTime;
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    Tiles[i, j].transform.position = Vector3.Lerp(Tiles[i, j].transform.position, TargetPoses[i, j], _speed * Time.deltaTime);
                }
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
        _mapIdx = ++_mapIdx % _mapImgs.Length;
        //ChangeMap(_mapImgs[_mapIdx]);
        StartCoroutine(ChangeMapCorutin(_mapImgs[_mapIdx]));
    }
    public void ChangeMap(Texture2D targetmap)
    {
        //var bArray1 = _test.GetRawTextureData<Color32>();
        var bArray = targetmap.GetPixels32(0);
        //string texAsString = Convert.ToBase64String(bArray);
        int playerCnt = 0;

        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                float heighttmp = (float)(bArray[(_size) * i + j]).r;
                TargetPoses[i, j] = transform.position + new Vector3(i * _tileSize, heighttmp == 0 ? -6 : heighttmp / _height, j * _tileSize);
                int a = (int)((float)(bArray[(_size) * i + j]).b) / 50;
                Tiles[i, j].ChangeTile(TileType.Dirt + a);

                if ((bArray[(_size) * i + j]).g == 255)
                {
                    if (playerCnt < GameManager.Instance.PlayerManagerCompos[0].GetCompo<PlayerAgentManager>().Units.Count)
                    {
                        AgentForceMoveCompo forceMov = _playerAgentManager.Units[playerCnt++].GetCompo<AgentForceMoveCompo>();
                        forceMov.SetTargetPos(transform.position + new Vector3(i * _tileSize, _heightOffset, j * _tileSize));
                        forceMov.SetMoveTime(0.8f);

                    }
                }

                if ((bArray[(_size) * i + j]).g != 0)
                {
                    //if (((bArray[(_size) * i + j]).a) < GameManager.Instance.PlayerManagerCompos[1].GetCompo<EnemyGenerator>().CurrentGenEnemyList.Units.Count)
                    GameManager.Instance.PlayerManagerCompos[1].GetCompo<EnemyGenerator>().GenEnemyies(transform.position +  new Vector3(i * _tileSize, _heightOffset, j * _tileSize), (int)(bArray[(_size) * i + j]).g / 50);
                }
                //tiles[i, j].material = _tileMats[];
            }
        }
        _time = 1.5f / _speed;
    }
    private IEnumerator ChangeMapCorutin(Texture2D targetmap)
    {
        //var bArray1 = _test.GetRawTextureData<Color32>();
        var bArray = targetmap.GetPixels32(0);
        //string texAsString = Convert.ToBase64String(bArray);
        int playerCnt = 0;

        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                float heighttmp = (float)(bArray[(_size) * i + j]).r;
                TargetPoses[i, j] = transform.position + new Vector3(i * _tileSize, heighttmp == 0 ? -6 : heighttmp / _height, j * _tileSize);
                int a = (int)((float)(bArray[(_size) * i + j]).b) / 50;
                Tiles[i, j].ChangeTile(TileType.Dirt + a);

                if ((bArray[(_size) * i + j]).g == 255)
                {
                    if (playerCnt < GameManager.Instance.PlayerManagerCompos[0].GetCompo<PlayerAgentManager>().Units.Count)
                    {
                        AgentForceMoveCompo forceMov = _playerAgentManager.Units[playerCnt++].GetCompo<AgentForceMoveCompo>();
                        forceMov.SetTargetPos(transform.position + new Vector3(i * _tileSize, _heightOffset, j * _tileSize));
                        forceMov.SetMoveTime(1.5f);

                    }
                }
                else
                if ((bArray[(_size) * i + j]).g != 0)
                {
                    //if (((bArray[(_size) * i + j]).a) < GameManager.Instance.PlayerManagerCompos[1].GetCompo<EnemyGenerator>().CurrentGenEnemyList.Units.Count)
                    GameManager.Instance.PlayerManagerCompos[1].GetCompo<EnemyGenerator>().GenEnemyies(transform.position + new Vector3(i * _tileSize, _heightOffset, j * _tileSize), (int)(bArray[(_size) * i + j]).g / 50);
                }
                //tiles[i, j].material = _tileMats[];
            }
        }
        yield return null;
        _time = 1.5f / _speed;
    }
}