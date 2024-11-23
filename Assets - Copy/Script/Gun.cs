using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 50f;
    public AudioClip shootSound;

    private AudioSource audioSource;
    public AmmoManager ammoManager; // Referensi ke AmmoManager

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = GetComponentInParent<AudioSource>();
        }

        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ammoManager != null && ammoManager.currentAmmo > 0)
            {
                Shoot();
                ammoManager.UseAmmo(); // Kurangi amunisi saat menembak
            }
            else
            {
                Debug.Log("No ammo left!");
            }
        }
    }

    private void Shoot()
    {
        // Spawn peluru
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        var rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = bulletSpawnPoint.forward * bulletSpeed;
        }

        // Mainkan suara tembakan
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
        else
        {
            Debug.LogWarning("AudioSource atau shootSound belum disiapkan!");
        }
    }
}
