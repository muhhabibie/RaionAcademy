using UnityEngine;
using UnityEngine.SceneManagement;

public class AmmoSpawner : MonoBehaviour
{
    public GameObject ammoBoxPrefab;  // Prefab ammo box
    public Transform player;          // Referensi ke Player
    public float currentInterval = 0f; // Waktu berjalan untuk spawn
    public float interval = 5f;        // Interval waktu spawn ammo box

    // Batas area spawn ammo box (disamakan dengan Spawner)
    public float minX = -10f; // Batas kiri arena
    public float maxX = 10f;  // Batas kanan arena
    public float minZ = -10f; // Batas bawah arena
    public float maxZ = 10f;  // Batas atas arena
    public float margin = 1f; // Margin untuk menjauhkan dari pinggir arena

    public bool gameOver = false;      // Status Game Over
    public Canvas ui;                  // UI untuk Game Over
    public int resetSpawnCount = 3;
    void Start()
    {
        // Nonaktifkan UI Game Over di awal
        if (ui != null)
        {
            ui.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Canvas UI belum di-assign!");
        }

        // Pastikan player sudah di-assign
        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogError("Player dengan tag 'Player' tidak ditemukan!");
            }
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        // Validasi batasan spawn untuk memastikan ammo tetap dalam arena
        float randomX = Random.Range(minX + margin, maxX - margin);
        float randomZ = Random.Range(minZ + margin, maxZ - margin);
        float spawnHeight = 1f; // Tinggi Y agar ammo box muncul di atas ground
        return new Vector3(
            Mathf.Clamp(randomX, minX + margin, maxX - margin),
            spawnHeight,
            Mathf.Clamp(randomZ, minZ + margin, maxZ - margin)
        );
    }

    void Update()
    {
        if (GameController.Instance != null && GameController.Instance.isGameRunning)
        {
            currentInterval += Time.deltaTime;
            if (currentInterval > interval)
            {
                currentInterval = 0;

                // Tentukan posisi spawn secara acak dalam arena
                SpawnAmmoBox();
            }
        }
        else
        {
            Debug.Log("Game is not running. AmmoSpawner paused.");
        }
    }
    public void ResetAmmoSpawner()
    {
        Debug.Log("Resetting Ammo Spawner...");

        // Hapus semua ammo yang telah di-spawn
        GameObject[] spawnedAmmo = GameObject.FindGameObjectsWithTag("AmmoBox");
        foreach (GameObject ammo in spawnedAmmo)
        {
            Destroy(ammo);
        }

        // Reset interval
        currentInterval = 0f;

        // Spawn ammo box secara manual (misalnya, 8 ammo box langsung)
        for (int i = 0; i < resetSpawnCount; i++)
        {
            SpawnAmmoBox();
        }

        Debug.Log($"{resetSpawnCount} ammo boxes spawned directly.");
    }

    // Fungsi untuk spawn ammo box
    private void SpawnAmmoBox()
    {
        if (ammoBoxPrefab == null)
        {
            Debug.LogError("Ammo box prefab is not assigned!");
            return;
        }

        // Tentukan posisi spawn secara acak dalam arena
        Vector3 spawnPosition = GetRandomSpawnPosition();

        // Spawn ammo box tanpa rotasi
        Instantiate(ammoBoxPrefab, spawnPosition, Quaternion.identity);

        Debug.Log("Spawned an ammo box.");
    }





    public void Quit()
    {
        Application.Quit();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
