using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnVisualUpdate : MonoBehaviour
{
    [SerializeField]
    GameObject _black, _white;
    private void Update()
    {
        _black.SetActive(NetPlayerMana.isBlackTurn.Value);
        _white.SetActive(!NetPlayerMana.isBlackTurn.Value);
    }
}
