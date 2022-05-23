using UnityEngine;
using MyOtherHalf.InputSystem;
using MyOtherHalf.HPSystem;


namespace MyOtherHalf.Characters
{
    public class PlayerController : BaseCharacterController, IHealthModifier
    {
        [SerializeField] private HealthUI healthUIReference;
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private float maxHealth = 5f;

        private InputManager inputManager;
        private HealthSystem healthSystem;

        private void Awake() 
        {
            inputManager = InputManager.Instance;
            healthSystem = new HealthSystem(maxHealth);
            healthUIReference?.SetupUI(healthSystem);
            healthSystem.HealToFull();
        }

        private void OnEnable() 
        {
            inputManager.OnLeftStickInput += Move;
            inputManager.OnFireInput += Shooting;
        }

        private void OnDisable() 
        {
            inputManager.OnLeftStickInput -= Move;
            inputManager.OnFireInput -= Shooting;
        }

        private void Move(Vector2 direction)
        {
            Movement(direction, movementSpeed);
        }

        private void Shooting(Vector2 direction)
        {
            FireProjectiles(direction, "bullet");
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

