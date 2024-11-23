using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f; // Nyawa maksimum Ghost
    public float currentHealth; // Nyawa saat ini
    public Renderer ghostRenderer; // Renderer dari Ghost
    public Color fullHealthColor = Color.green; // Warna saat nyawa penuh
    public Color lowHealthColor = Color.red; // Warna saat nyawa hampir habis

    void Start()
    {
        currentHealth = maxHealth;

        if (ghostRenderer == null)
        {
            ghostRenderer = GetComponent<Renderer>(); // Ambil Renderer secara otomatis jika belum diassign
        }
    }

    void Update()
    {
        // Pastikan warna Ghost berubah berdasarkan nyawa
        UpdateGhostColor();
    }

    // Fungsi untuk menerima damage
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Pastikan nyawa tidak melebihi batas

        // Jika nyawa habis, hancurkan ghost
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Update warna Ghost berdasarkan nyawa
    void UpdateGhostColor()
    {
        if (ghostRenderer != null)
        {
            float healthPercentage = currentHealth / maxHealth;
            Color currentColor = Color.Lerp(lowHealthColor, fullHealthColor, healthPercentage);

            Debug.Log($"Current Health: {currentHealth}, Color: {currentColor}");
            ghostRenderer.material.color = currentColor;
        }
        else
        {
            Debug.LogWarning("Renderer tidak ditemukan!");
        }
    }


    // Fungsi jika Ghost mati
    void Die()
    {
        Debug.Log("Ghost mati!");

        // Tambahkan kill ke ScoreManager
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddKill();
        }

        Destroy(gameObject); // Hancurkan Ghost
    }

}
