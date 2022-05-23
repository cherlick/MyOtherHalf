using System;
using UnityEngine;
using UnityEngine.UI;

namespace MyOtherHalf.HPSystem
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private Image fillImage;
        [SerializeField] private Slider barSlider;
        [SerializeField] private Gradient gradient;
        [SerializeField] private bool useSmoothChanges;
        [SerializeField] private float lerpSpeed;
        private HealthSystem healthSystem;

        public void SetupUI(HealthSystem newHealthSystem)
        {
            healthSystem = newHealthSystem;
            healthSystem.OnHealthChanged += OnHealthChanged;

            barSlider.maxValue = healthSystem.GetMaxHeath;
            barSlider.value = healthSystem.GetMaxHeath;

            fillImage.color = gradient.Evaluate(1f);

            iconImage?.gameObject.SetActive(true);
        }

        private void OnHealthChanged(object sender, EventArgs e)
        {
            barSlider.value = useSmoothChanges 
                ? Mathf.Lerp(barSlider.value, healthSystem.GetCurrentHealth, lerpSpeed)
                :barSlider.value = healthSystem.GetCurrentHealth;

            fillImage.color = gradient.Evaluate(barSlider.normalizedValue);
        }
    }
}

