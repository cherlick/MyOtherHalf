using UnityEngine;
using MyOtherHalf.InputSystem;
using UnityEngine.Rendering.Universal;
using MyOtherHalf.HPSystem;


namespace MyOtherHalf.Characters
{
    public class PlayerController : BaseCharacterController, IHealthModifier
    {
        [SerializeField] private HealthUI healthUIReference;
        [SerializeField] private Light2D backLight2D;

        private InputManager inputManager;
        private HealthSystem healthSystem;
        private SpriteRenderer spriteRenderer;

        public CharacterData SetData 
        {
            set 
            {
                characterData = value;
                Debug.Log($"{characterData}");
                gameObject.SetActive(true);
            } 
        }

        protected override void Awake() 
        {
            base.Awake();
            inputManager = InputManager.Instance;
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable() 
        {
            inputManager.OnLeftStickInput += Move;
            inputManager.OnFireInput += Shooting;
            Debug.Log($"{characterData}");
            healthSystem = new HealthSystem(characterData.baseMaxHealth);
            healthUIReference?.SetupUI(healthSystem);
            healthSystem.HealToFull();

            backLight2D = GetComponentInChildren<Light2D>();
            backLight2D.color = characterData.backgroundLightColor;

            spriteRenderer.sprite = characterData.characterSprite;
        }

        private void OnDisable() 
        {
            inputManager.OnLeftStickInput -= Move;
            inputManager.OnFireInput -= Shooting;
        }

        private void Move(Vector2 direction)
        {
            Movement(direction, characterData.movementSpeed);
        }

        private void Shooting(Vector2 direction)
        {
            FireProjectiles(direction, "bullet", characterData.baseDamage);
        }

        public void TakeDamage(float damageAmount)
        {
            healthSystem.Damage(damageAmount);
        }

        public void Healling(float heallingAmount)
        {
            healthSystem.Heal(heallingAmount);
        }
    }
}

