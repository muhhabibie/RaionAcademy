using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public float movementSpeed = 5.0f; // Kecepatan gerak pemain
    private float maxDistance = 5f;  // Batas maksimum di sumbu X

    [Header("Bullet Settings")]
    public GameObject bulletPrefab;   // Referensi ke prefab peluru
    public Transform bulletSpawnPoint; // Titik tempat peluru muncul
    public int ammoCount = 10;        // Jumlah awal peluru

    [Header("Ammo Box Settings")]
    public int ammoPerPickup = 5;    // Jumlah peluru yang didapat per ammo box

    void Update()
    {
        // Ambil input horizontal dari keyboard
        float inputHorizontal = 0;

        if (Input.GetKey(KeyCode.A)) // Jika tombol A ditekan
        {
            inputHorizontal = 1; // Gerak ke kiri
        }
        else if (Input.GetKey(KeyCode.D)) // Jika tombol D ditekan
        {
            inputHorizontal = -1; // Gerak ke kanan
        }

        // Hitung posisi baru pemain
        Vector3 movementDirection = new Vector3(inputHorizontal, 0, 0); // Gerakan hanya di sumbu X
        Vector3 newPosition = transform.position + movementDirection * movementSpeed * Time.deltaTime;

        // Pastikan posisi X pemain tetap berada dalam batas maxDistance
        newPosition.x = Mathf.Clamp(newPosition.x, -maxDistance, maxDistance);

        // Tetapkan posisi baru pemain (tetap mempertahankan posisi Z saat ini)
        newPosition.z = transform.position.z; // Z tidak berubah

        // Terapkan posisi baru
        transform.position = newPosition;

        // Penembakan (opsional, sama seperti sebelumnya)
        if (Input.GetKeyDown(KeyCode.Space) && ammoCount > 0)
        {
            ShootBullet();
            ammoCount--; // Kurangi jumlah peluru
        }
    }

    private void ShootBullet()
    {
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            // Spawn peluru
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            // Tambahkan kecepatan peluru jika peluru memiliki Rigidbody
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                bulletRb.linearVelocity = bulletSpawnPoint.forward * 20f; // Sesuaikan kecepatan peluru
            }
        }
    }

    public void AddAmmo(int amount)
    {
        ammoCount += amount; // Tambahkan jumlah peluru

        // Perbarui UI jika menggunakan AmmoManager
        AmmoManager ammoManager = FindObjectOfType<AmmoManager>();
        if (ammoManager != null)
        {
            ammoManager.ReloadAmmo(amount); // Tambahkan ammo ke AmmoManager
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("AmmoBox"))
        {
            AddAmmo(ammoPerPickup);
            Destroy(other.gameObject);
        }
    }
}
