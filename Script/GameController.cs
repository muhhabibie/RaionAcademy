using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [Header("UI Canvases")]
    public GameObject menu;  // Canvas untuk Main Menu
    public GameObject hud;      // Canvas untuk HUD
    public GameObject GameOverCanvas; // Canvas untuk Game Over

    [Header("Game State")]
    public bool isGameRunning = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Hanya untuk GameController, bukan UI
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Cari Canvas di scene saat ini
        UpdateCanvasReferences();
    }
    public void NewGame()
    {
        isGameRunning = true;
        Debug.Log("Game Started");

        // Atur visibilitas Canvas
        if (menu != null) menu.SetActive(false);
        if (hud != null) hud.SetActive(true);
        if (GameOverCanvas != null) GameOverCanvas.SetActive(false);

        // Mulai permainan
        Time.timeScale = 1f;
    }

    public void StartGame()
    {
        // Set status permainan ke aktif
        isGameRunning = true;
        Debug.Log("Game Started");

        // Atur visibilitas Canvas
        if (menu != null) menu.SetActive(false);  // Sembunyikan menu utama
        if (hud != null) hud.SetActive(true);     // Tampilkan HUD untuk gameplay
        if (GameOverCanvas != null) GameOverCanvas.SetActive(false); // Sembunyikan Game Over Canvas

        // Pastikan waktu berjalan normal
        Time.timeScale = 1f;

        // Debug untuk memeriksa apakah permainan benar-benar dimulai
        Debug.Log("Game is now running, all systems go.");
    }





    public void UpdateCanvasReferences()
    {
        // Cari objek Canvas berdasarkan nama di Hierarchy
        menu = GameObject.Find("menu");
        hud = GameObject.Find("hud");
        GameOverCanvas = GameObject.Find("GameOverCanvas");

        // Debug untuk memeriksa apakah referensi ditemukan
        if (menu == null) Debug.LogWarning("MainMenuCanvas tidak ditemukan!");
        if (hud == null) Debug.LogWarning("HudCanvas tidak ditemukan!");
        if (GameOverCanvas == null) Debug.LogWarning("GameOverCanvas tidak ditemukan!");
    }

    public void RetryGame()
    {
        isGameRunning = true; // Ini sudah ada

        Debug.Log("Retry Game");
        Time.timeScale = 1f;
        Debug.Log("Time.timeScale set to: " + Time.timeScale);

        // Reset variabel permainan
        ScoreManager.Instance.ResetScore();

        // Reset health (asumsi ada komponen PlayerHealth yang mengelola health)
        HealthLogic playerHealth = FindObjectOfType<HealthLogic>();
        if (playerHealth != null)
        {
            playerHealth.ResetHealth();
        }

        // Reset ammo (asumsi ada komponen AmmoManager yang mengelola ammo)
        AmmoManager ammoManager = FindObjectOfType<AmmoManager>();
        if (ammoManager != null)
        {
            ammoManager.ResetAmmo();
        }

        // Reset AmmoSpawner
        AmmoSpawner ammoSpawner = FindObjectOfType<AmmoSpawner>();
        if (ammoSpawner != null)
        {
            ammoSpawner.ResetAmmoSpawner();
        }

        // Reset GhostSpawner agar musuh bisa respawn menggunakan interval
        Spawner ghostSpawner = FindObjectOfType<Spawner>();
        if (ghostSpawner != null)
        {
           // Pastikan komponen aktif
            ghostSpawner.ResetEnemySpawner();
          
        }

        // Atur visibilitas Canvas
        if (menu != null) menu.SetActive(false);
        if (hud != null) hud.SetActive(true);
        if (GameOverCanvas != null) GameOverCanvas.SetActive(false);

        // Atur status permainan
      
        Debug.Log("isGameRunning set to: " + isGameRunning);

        // Pastikan waktu berjalan normal
        Time.timeScale = 1f;
        Debug.Log("Time.timeScale set to: " + Time.timeScale);

    }


    public void Quit()
    {
        Debug.Log("Game is quitting...");
        Application.Quit();
    }

    public void GameOver()
{
    Debug.Log("Game Over");
    isGameRunning = false;

    // Atur visibilitas Canvas
    if (menu != null) menu.SetActive(false);
    if (hud != null) hud.SetActive(false);
    if (GameOverCanvas != null) GameOverCanvas.SetActive(true);

    // Hentikan waktu
    Time.timeScale = 0f;
}


}
