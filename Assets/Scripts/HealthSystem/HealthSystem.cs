using System;

namespace MyOtherHalf.HPSystem
{
    public class HealthSystem
    {
        public event EventHandler OnHealthChanged;
        private float maxHealth;
        private float currentHealth;

        public float GetMaxHeath => maxHealth;
        public float GetCurrentHealth => currentHealth;

        public HealthSystem(float newMaxHealth)
        {
            maxHealth = newMaxHealth;
        }
        
        public float GetHealthPercent() => (float) currentHealth / maxHealth;

        public void Damage(float damageAmount)
        {
            currentHealth -= damageAmount;
            currentHealth = currentHealth < 0 ? 0 : currentHealth;
            ChangedInHealth();
        }

        public void Heal(float healAmount)
        {
            currentHealth += healAmount;
            currentHealth = currentHealth > maxHealth ? maxHealth : currentHealth;
            ChangedInHealth();
        }

        public void HealToFull()
        {
            currentHealth = maxHealth;
        }

        private void ChangedInHealth()
        {
            if (OnHealthChanged != null)
            {
                OnHealthChanged(this, EventArgs.Empty);
            }
        }

    }
}
