using UnityEngine;
using UnityEngine.Rendering.Universal;
using MyOtherHalf.InputSystem;
using System;

namespace MyOtherHalf.Characters
{
    public class HalfPartController : BaseCharacterController
    {
        public static Action<Vector2> OnHalfCollision;
        public static Action OnStepDone;

        [SerializeField] private Light2D light2D;
        [SerializeField] private CharacterData data;
        private SpriteRenderer halfSpriteRender;
        private InputManager inputManager;
        
        protected override void Awake() 
        {
            base.Awake();
            inputManager = InputManager.Instance;

            light2D = GetComponentInChildren<Light2D>();
            light2D.color = data.backgroundLightColor;

            halfSpriteRender = GetComponent<SpriteRenderer>();
            halfSpriteRender.sprite = data.characterSprite;
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
            OnStepDone?.Invoke();
        }

        private void OnCollisionEnter2D(Collision2D other) 
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Merge");
                OnHalfCollision?.Invoke(transform.position);
                gameObject.SetActive(false);
            }    
        }
    }
}

