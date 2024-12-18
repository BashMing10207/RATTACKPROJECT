using System;
using UnityEngine;
using UnityEngine.InputSystem;


    [CreateAssetMenu(fileName = "PlayerInputSO", menuName = "SO/Player/InputSO")]
    public class PlayerInputSO : ScriptableObject, Input2.IPlayerActions, Input2.IUIActions
    {
        public event Action JumpEvent;
        public event Action AttackEvent;
        public event Action OpenMenuKeyEvent;
        public event Action TurnEndEvent;
        public Action<int> UnitSwapEvent;
        public event Action OnClickEnter;
        public event Action OnClickExit;

        public Vector2 InputDirection {get; private set;}
        
        public Vector2 MousePos {get; private set;}
        private Input2 _controls;


        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Input2();
                _controls.Player.SetCallbacks(this);
                _controls.UI.SetCallbacks(this);
            }
            _controls.Player.Enable();
            _controls.UI.Enable();
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
            _controls.UI.Disable();
        }

        
        public void SetPlayerInput(bool isEnable)
        {
            if(isEnable)
                _controls.Player.Enable();
            else
                _controls.Player.Disable();
        }

        public void OnChange(InputAction.CallbackContext context)
        {
            if(context.performed)
            TurnEndEvent?.Invoke();
        }

        public void OnMousePos(InputAction.CallbackContext context)
        {
            MousePos = context.ReadValue<Vector2>();
        }

        public void OnMouseButton(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnClickEnter?.Invoke();
            if (context.canceled)
                OnClickExit?.Invoke();
        }

        public void OnSwap(InputAction.CallbackContext context)
        {
            if(context.performed)
                UnitSwapEvent?.Invoke(context.ReadValue<int>());
        }

        public void OnEsc(InputAction.CallbackContext context)
        {
            if (context.performed)
                OpenMenuKeyEvent?.Invoke();
        }

        public void OnNavigate(InputAction.CallbackContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnSubmit(InputAction.CallbackContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnPoint(InputAction.CallbackContext context)
        {
            //throw new NotImplementedException();
        }

        void Input2.IUIActions.OnClick(InputAction.CallbackContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnMiddleClick(InputAction.CallbackContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnScrollWheel(InputAction.CallbackContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnTrackedDevicePosition(InputAction.CallbackContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnOpenMenu(InputAction.CallbackContext context)
        {
            //throw new NotImplementedException();
        }
    }
