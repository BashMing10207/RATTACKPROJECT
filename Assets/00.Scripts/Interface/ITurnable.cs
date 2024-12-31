using UnityEngine;

public interface ITurnable //멀티플레이어 / 싱글플레이어에 각각 필요한 턴메니저에 사용.
{
    public void TurnChange();

    public bool CurrentTurn();
}
