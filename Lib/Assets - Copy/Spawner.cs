using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public GhostMovement enemyPrefab; // Prefab Ghost
    public float currentInterval = 0f; // Waktu berjalan untuk spawn
    public float interval = 2f;        // Interval waktu spawn Ghost

    // Batas area spawn Ghost
    public float minX = -10f; // Batas kiri arena
    public float maxX = 10f;  // Batas kanan arena
    public float minZ = -10f; // Batas bawah arena
    public float maxZ = 10f;  // Batas atas arena
    public float margin = 1f; // Margin untuk menjauhkan dari pinggir arena

    public bool gameOver = false;      // Status Game Over
    public int resetSpawnCount = 6;

    void Start()
    {
        Debug.Log("Spawner initialized.");
        currentInterval = 0f; // Reset interval saat game mulai
    }


    public void ResetEnemySpawner()
    {
        Debug.Log("Resetting Enemy Spawner...");

        // Bersihkan semua musuh yang ada
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        // Reset status spawner
        gameOver = false;

        // Spawn musuh secara manual (8 musuh dengan jeda waktu)
        for (int i = 0; i < resetSpawnCount; i++)
        {
            SpawnEnemy();
        }
    }


    // Fungsi untuk spawn enemy
    private void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab is not assigned!");
            return;
        }

        // Tentukan posisi spawn secara acak dalam arena
        Vector3 spawnPosition = GetRandomSpawnPosition();

        // Spawn Ghost tanpa rotasi
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        Debug.Log("Spawned an enemy.");
    }

    Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(minX + margin, maxX - margin);
        float randomZ = Random.Range(minZ + margin, maxZ - margin);
        float spawnHeight = 1f; // Tinggi Y agar Ghost muncul di atas ground
        return new Vector3(randomX, spawnHeight, randomZ);
    }

    void Update()
    {
       

        if (!gameOver && GameController.Instance != null && GameController.Instance.isGameRunning)
        {
            currentInterval += Time.deltaTime;
            if (currentInterval > interval)
            {
                currentInterval = 0;
                SpawnEnemy();
            }
        }
        else
        {
            

        }
    }
}