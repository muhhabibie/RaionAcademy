using UnityEngine;
using TMPro; // Tambahkan namespace TextMeshPro

public class AmmoManager : MonoBehaviour
{
    public int maxAmmo = 30; // Kapasitas maksimum amunisi
    public int currentAmmo;  // Amunisi saat ini
    public TextMeshProUGUI ammoText; // Referensi ke UI TextMeshPro untuk menampilkan amunisi

    void Awake()
    {
        // Debugging awal untuk memeriksa apakah ammoText diassign sebelum Start
        if (ammoText == null)
        {
            Debug.LogWarning("Ammo Text UI belum di-assign di Awake! Pastikan untuk mengisi di Inspector.");
        }
    }

    void Start()
    {
        if (ammoText == null)
        {
            // Mencari elemen dengan nama tertentu
            GameObject foundObject = GameObject.Find("AmmoText");
            if (foundObject != null)
            {
                ammoText = foundObject.GetComponent<TextMeshProUGUI>();
                Debug.Log($"Ammo Text ditemukan di runtime: {ammoText.name}");
            }
            else
            {
                Debug.LogError("Ammo Text UI tidak ditemukan di runtime!");
            }
        }

        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    public void UseAmmo()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
            UpdateAmmoUI();
        }
        else
        {
            Debug.Log("Out of ammo!");
        }
    }
    public void ResetAmmo()
    {
        currentAmmo = maxAmmo; // Reset ammo ke kapasitas maksimum
        UpdateAmmoUI(); // Perbarui UI setelah reset
        Debug.Log("Ammo has been reset to maximum: " + currentAmmo);
    }

    public void ReloadAmmo(int ammoAmount)
    {
        currentAmmo = Mathf.Clamp(currentAmmo + ammoAmount, 0, maxAmmo);
        UpdateAmmoUI();
    }

    private void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            Debug.Log($"Updating Ammo Text: {currentAmmo} on {ammoText.name}");
            ammoText.text = $"{currentAmmo}";
        }
        else
        {
            Debug.LogWarning("Ammo Text UI belum di-assign!");
        }
    }
}
