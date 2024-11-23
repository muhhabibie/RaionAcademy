using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraLogic : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        if (player == null)
        {
            // Cari objek Player di scene
            player = GameObject.FindGameObjectWithTag("Player");
        }

        // Tambahkan listener untuk scene loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Lepaskan listener untuk menghindari error
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        if (player != null)
        {
            // Ikuti posisi player
            transform.position = player.transform.position;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Perbarui referensi player setelah scene dimuat
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
