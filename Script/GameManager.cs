using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton untuk akses global
    public bool isGameRunning = false;  // Status apakah game sedang berjalan

    void Awake()
    {
        // Pastikan hanya ada satu instance GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        isGameRunning = true; // Mulai game
        Debug.Log("Game Started! isGameRunning: " + isGameRunning);
    }

    public void StopGame()
    {
        isGameRunning = false; // Hentikan game
        Debug.Log("Game Stopped! isGameRunning: " + isGameRunning);
    }

}
