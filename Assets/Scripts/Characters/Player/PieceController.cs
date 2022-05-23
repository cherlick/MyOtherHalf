using UnityEngine;
using MyOtherHalf.InputSystem;

namespace MyOtherHalf.Characters
{
    public class PieceController : BaseCharacterController
    {
        private InputManager inputManager;
        private void Awake() 
        {
            inputManager = InputManager.Instance;
        }

        private void OnEnable() 
        {
            inputManager.OnSwipeTouchInput += Move;
        }

        private void OnDisable() 
        {
            inputManager.OnSwipeTouchInput -= Move;
        }

        private void Move(Vector2 direction)
        {
            if (direction == Vector2.zero)
            {
                return;
            }

            float xDirection = direction.x;
            float yDirection = direction.y;

            if (Mathf.Abs(xDirection) > Mathf.Abs(yDirection))
            {
                direction.x = xDirection > 0 ? 1 : -1;
                direction.y = 0;
            }
            else
            {
                direction.x = 0;
                direction.y = yDirection > 0 ? 1 : -1;
            }

            StepsMove(direction);
        }
    }
}

