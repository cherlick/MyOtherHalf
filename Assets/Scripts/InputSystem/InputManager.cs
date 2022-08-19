using UnityEngine;
using UnityEngine.InputSystem;
using MyOtherHalf.Tools;
using MyOtherHalf.Core;
using System;

namespace MyOtherHalf.InputSystem
{
    [DefaultExecutionOrder (-1)]
    public class InputManager : Singleton<InputManager>
    {

        #region Events

        public delegate void SwipeEvent(Vector2 direction);
        public event SwipeEvent OnSwipeTouchInput;
        public delegate void LeftStickEvent(Vector2 direction);
        public event LeftStickEvent OnLeftStickInput;
        public delegate void RightStickClickEvent(Vector2 direction);
        public event RightStickClickEvent OnFireInput;

        #endregion
        
        [SerializeField] private Camera cam;
        private PlayerInputActions playerInputActions;
        private Vector2 swipeDirection;
        private Vector2 lookingDirection;
        

        private void Awake() 
        {
            playerInputActions = new PlayerInputActions();

            cam = cam == null ? Camera.main : cam;
        }

        private void OnEnable() 
        {
            playerInputActions.Enable();
            playerInputActions.PuzzleMode.Click.canceled += EndTouch;
            playerInputActions.PuzzleMode.WASDPress.started += EndTouch;
            playerInputActions.PuzzleMode.Move.performed += OnPuzzleMove;

            playerInputActions.BattleMode.Fire.performed += OnFire;
            playerInputActions.BattleMode.Look.performed += OnLooking;
            playerInputActions.BattleMode.Disable();

            GameManager.OnGameStateChange += ChangeActionMap;
        }

        private void OnDisable() 
        {
            playerInputActions.PuzzleMode.Click.canceled -= EndTouch;
            playerInputActions.PuzzleMode.WASDPress.started -= EndTouch;
            playerInputActions.PuzzleMode.Move.performed -= OnPuzzleMove;

            playerInputActions.BattleMode.Fire.performed -= OnFire;
            playerInputActions.BattleMode.Look.performed -= OnLooking;
            playerInputActions.Disable();

            GameManager.OnGameStateChange -= ChangeActionMap;
        }

        private void FixedUpdate() 
        {
            BattleMove(playerInputActions.BattleMode.Move.ReadValue<Vector2>());
        }

        private void EndTouch(InputAction.CallbackContext context)
        {
            OnSwipeTouchInput?.Invoke(swipeDirection);
        }

        private void BattleMove(Vector2 direction)
        {
            OnLeftStickInput?.Invoke(direction);
        }

        private void OnFire(InputAction.CallbackContext context)
        {
            Debug.Log($"OnFire");
            OnFireInput?.Invoke(lookingDirection);
            lookingDirection = Vector2.zero;
        }


        private void OnLooking(InputAction.CallbackContext context)
        {
            Debug.Log($"OnLooking {context.ReadValue<Vector2>()}");
            Vector2 direction = context.ReadValue<Vector2>();
            lookingDirection = direction != Vector2.zero ? direction : lookingDirection;
        }

        private void OnPuzzleMove(InputAction.CallbackContext context)
        {
            swipeDirection = Vector2.zero;
            Vector2 direction = context.ReadValue<Vector2>();

            swipeDirection = direction.magnitude !=0 ? direction : swipeDirection;
        }

        private void ChangeActionMap(GameState state)
        {
            switch (state)
            {
                case GameState.Pause:
                case GameState.StartMenu:
                    playerInputActions.UIMode.Enable();
                    playerInputActions.PuzzleMode.Disable();
                    playerInputActions.BattleMode.Disable();
                break;

                case GameState.PuzzleMode:
                    playerInputActions.UIMode.Disable();
                    playerInputActions.PuzzleMode.Enable();
                    playerInputActions.BattleMode.Disable();
                break;

                case GameState.BattleMode:
                    playerInputActions.UIMode.Disable();
                    playerInputActions.BattleMode.Enable();
                    playerInputActions.PuzzleMode.Disable();
                break;
            }
        }
    }
}

