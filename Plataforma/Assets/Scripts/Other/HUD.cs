using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    private int maxHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {        
        GameController.OnPlayerSpawned += SetupHealthBar;
        PlayerHealth.OnPlayerHealthChanged += UpdateHealthBar;
    }

    private void OnDisable()
    {
        GameController.OnPlayerSpawned -= SetupHealthBar;
        PlayerHealth.OnPlayerHealthChanged -= UpdateHealthBar;
    }

    private void SetupHealthBar(GameObject player)
    {
        healthBar.value = healthBar.maxValue;
        maxHealth = player.GetComponent<PlayerHealth>().maxHealth;
    }

    private void UpdateHealthBar(int currentHealth)
    {
        healthBar.value = (float)currentHealth / maxHealth;
        healthBar.value = Mathf.Clamp01(healthBar.value);
    }
}
