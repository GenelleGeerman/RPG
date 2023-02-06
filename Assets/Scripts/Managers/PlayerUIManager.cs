using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUIManager : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image healthFill;
    [SerializeField] private Gradient healthGradient;

    [Header("Essence")]
    [SerializeField] private Slider essenceSlider;
    [SerializeField] private Image essenceFill;
    [SerializeField] private Gradient essenceGradient;

    private EssenceManager essenceManager;
    private PlayerStats stats;

    private void Start()
    {
        stats = FindObjectOfType<PlayerStats>();
        essenceManager = FindObjectOfType<EssenceManager>();
        healthSlider.maxValue = stats.MaxHealth;
        essenceSlider.maxValue = essenceManager.MaxEssence;
    }

    private void Update()
    {
        HealthSliderUpdate();
        EssenceSliderUpdate();
    }

    private void HealthSliderUpdate()
    {
        healthSlider.value = stats.Health;
        healthFill.color = healthGradient.Evaluate(healthSlider.normalizedValue);
    }
    private void EssenceSliderUpdate()
    {
        essenceSlider.value = essenceManager.Essence;
        essenceFill.color = essenceGradient.Evaluate(essenceSlider.normalizedValue);
    }
}
