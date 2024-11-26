using UnityEngine;

public class NetGameMana: MonoBehaviour
{
    public bool isMultiplayer = true;

    static bool ISMUTIPLAYER;

    public static NetGameMana Instance;
    
    public NetPlayerMana player;
    public Player playerOff;
    public NetPool pool;
    
    
    public GameObject playerHand;
    public TestLobby relayMana;

    public GameObject win, lose;

    public AudioSource dieSo, AtSo;

    public float gravity = 9.8f;
    //SkillManaV2 skillManaV2;
    private void Awake()
    {
        Instance = this;
        ISMUTIPLAYER = isMultiplayer;
        
    }
    public static bool H_ISMULTI()//핸..들...?
    {
        return ISMUTIPLAYER;
    }
}
