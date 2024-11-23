using UnityEngine;

public class AmmoMovement : MonoBehaviour
{
    [Header("Ammo Box Settings")]
    public float speed = 3f;          // Kecepatan gerak Ammo Box
    public int ammoAmount = 10;       // Jumlah peluru yang ditambahkan ke pemain

    private bool hasExitedArena = false; // Flag untuk memastikan hanya satu trigger

    private void Update()
    {
        // Ammo box bergerak lurus ke arah depan
        transform.position += transform.forward * speed * Time.deltaTime;

        // Kunci sumbu Y agar tetap di ketinggian tertentu
        transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Deteksi jika Ammo Box melewati ArenaLimit
        if (other.CompareTag("ArenaLimit") && !hasExitedArena)
        {
            hasExitedArena = true; // Tandai Ammo Box telah keluar arena
            Debug.Log("Ammo Box melewati batas ArenaLimit dan dihancurkan.");
            Destroy(gameObject); // Hancurkan ammo box
        }

        // Cek apakah objek yang menabrak adalah Player
        if (other.CompareTag("Player"))
        {
            // Memastikan objek yang menabrak adalah Player dan memiliki komponen PlayerLogic
            PlayerLogic playerLogic = other.GetComponent<PlayerLogic>();
            if (playerLogic != null)
            {
                // Tambahkan ammo ke pemain
                playerLogic.AddAmmo(ammoAmount);
                Debug.Log($"Player menambah {ammoAmount} peluru!");

                // Hancurkan ammo box setelah diambil
                Destroy(gameObject);
            }
        }
    }
}
