using UnityEngine;
using UnityEngine.UI;

public class HealthLogic : MonoBehaviour
{
    [Header("Health Settings")]
    public Slider healthSlider; // Slider utama
    public Slider decreaseSlider; // Slider animasi
    public float maxHealth = 100f; // Health maksimum
    public float decreaseSpeed = 2f; // Kecepatan animasi slider menurun

    private float currentHealth; // Health saat ini

    private void Start()
    {
        ResetHealth(); // Set nyawa ke nilai awal di awal permainan
    }


    void Update()
    {
        // Update animasi decrease slider
        if (decreaseSlider.value > healthSlider.value)
        {
            decreaseSlider.value = Mathf.Lerp(decreaseSlider.value, healthSlider.value, Time.deltaTime * decreaseSpeed);
        }
    }

    // Fungsi untuk menerima damage
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Update slider health
        healthSlider.value = currentHealth;

        // Trigger efek kamera shake
        CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();
        if (cameraShake != null)
        {
            cameraShake.TriggerShake();
        }

        // Periksa apakah health habis
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth; // Reset ke nyawa maksimum
        UpdateHealthUI(); // Perbarui slider nyawa
        Debug.Log("Health reset to max: " + maxHealth);

        if (healthSlider != null && decreaseSlider != null)
        {
            healthSlider.value = maxHealth; // Atur slider utama ke nilai maksimum
            decreaseSlider.value = maxHealth; // Atur slider animasi ke nilai maksimum
        }
    }

    public void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth; // Atur nilai slider langsung ke currentHealth
        }
        else
        {
            Debug.LogWarning("Health slider is not assigned!");
        }
    }



    private void Die()
    {
        Debug.Log("Player mati!");
        GameController gameController = FindObjectOfType<GameController>();
        if (gameController != null)
        {
            gameController.GameOver(); // Panggil fungsi Game Over
        }
    }

}
