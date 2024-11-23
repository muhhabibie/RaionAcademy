using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton untuk akses global
    public int kills = 0; // Jumlah kill awal

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Jangan hancurkan objek saat berpindah scene
        }
        else
        {
            Destroy(gameObject); // Hancurkan duplikat
        }
    }
    public void ResetScore()
{
    Debug.Log("Resetting Score...");
    kills = 0;
}


    public void AddKill()
    {
        kills++;
        Debug.Log("Jumlah Kill: " + kills);
    }
}
