using UnityEngine;

public class bulletLogic : MonoBehaviour
{
    public float life = 3f; // Waktu hidup peluru (detik)
    public AudioClip hitSound; // Suara ketika mengenai Ghost
    private AudioSource audioSource;
    public GameObject hitEffect; // Prefab partikel untuk efek kena target


    void Awake()
    {
        // Hancurkan peluru setelah waktu tertentu
        Destroy(gameObject, life);
    }

    void Start()
    {
        // Ambil atau tambahkan AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Cari komponen GhostHealth di objek yang terkena peluru
        var ghost = collision.gameObject.GetComponent<GhostHealth>();
        if (ghost != null)
        {
            ghost.TakeDamage(10); // Berikan damage ke target

            // Tampilkan efek hit
            if (hitEffect != null)
            {
                Instantiate(hitEffect, collision.contacts[0].point, Quaternion.identity);
            }
        }

        // Hancurkan peluru setelah mengenai sesuatu
        Destroy(gameObject);
    }

}
