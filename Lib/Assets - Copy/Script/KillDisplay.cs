using UnityEngine;
using TMPro; // Jika menggunakan TextMeshPro

public class KillDisplay : MonoBehaviour
{
    public TextMeshProUGUI killText; // TextMeshPro untuk UI Kill Count

    void Update()
    {
        if (ScoreManager.Instance != null)
        {
            killText.text = "Kills: " + ScoreManager.Instance.kills; // Perbarui teks jumlah kill
        }
    }
}
