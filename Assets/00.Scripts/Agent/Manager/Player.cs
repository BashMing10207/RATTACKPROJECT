using UnityEngine.InputSystem;

public class Player : GetCompoParent
{
    //public PlayerInputSO PlayerInput;

    protected override void Awake()
    {
        base.Awake();

    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            GameManager.Instance.OnTurnEnd();
        }
    }
}

