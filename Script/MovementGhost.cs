using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 3f; // Kecepatan Ghost
    public float damageToPlayer = 20f; // Damage yang diberikan ke player saat Ghost lolos

    private bool hasEscaped = false; // Mencegah multiple damage

    void Update()
    {
        // Gerakan Ghost maju (ke arah depan)
        transform.position += transform.forward * speed * Time.deltaTime;

        // Kunci sumbu Y agar tetap di posisi tertentu
        transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Deteksi jika Ghost melewati ArenaLimit
        if (other.CompareTag("ArenaLimit") && !hasEscaped)
        {
            hasEscaped = true; // Tandai Ghost telah lolos
            DamagePlayer(); // Berikan damage ke pemain
        }
    }

    private void DamagePlayer()
    {
        // Cari Player dengan tag "Player"
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            // Ambil komponen HealthLogic pada Player
            HealthLogic healthLogic = playerObject.GetComponent<HealthLogic>();
            if (healthLogic != null)
            {
                // Berikan damage
                healthLogic.TakeDamage(damageToPlayer);

                // Trigger camera shake
                CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();
                if (cameraShake != null)
                {
                    cameraShake.TriggerShake();
                }

                Debug.Log($"Player terkena damage sebesar {damageToPlayer} dari Ghost!");

                // Hancurkan Ghost setelah memberikan damage
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("HealthLogic tidak ditemukan pada Player.");
            }
        }
        else
        {
            Debug.LogError("Player tidak ditemukan di scene!");
        }
    }

}
