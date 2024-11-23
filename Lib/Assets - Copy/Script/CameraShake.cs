using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.2f;  // Durasi shake
    public float shakeMagnitude = 0.3f; // Intensitas shake

    private Vector3 originalPosition;
    private float shakeTimer = 0f;

    private void Start()
    {
        originalPosition = transform.localPosition; // Simpan posisi awal kamera
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            // Lakukan efek shake
            transform.localPosition = originalPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            // Kembalikan posisi kamera ke posisi awal
            transform.localPosition = originalPosition;
        }
    }

    public void TriggerShake()
    {
        shakeTimer = shakeDuration; // Atur timer untuk memulai shake
    }
}
