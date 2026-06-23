using UnityEngine;
using TMPro;

public class QuizStartManager : MonoBehaviour
{
    [Header("Referensi UI")]
    public GameObject panelQuizAwal;
    public TextMeshProUGUI textSoalKuis;

    [Header("Input Soal Kuis Level Ini")]
    [TextArea(4, 10)] 
    public string teksPertanyaanKuis;

    void Start()
    {
        if (panelQuizAwal != null)
        {
            panelQuizAwal.SetActive(true);
            textSoalKuis.text = teksPertanyaanKuis;
            Time.timeScale = 0f; 
        }
    }

    // Fungsi saat Tombol Mulai Kuis diklik
    public void MulaiCariObjek()
    {
        if (panelQuizAwal != null)
        {
            panelQuizAwal.SetActive(false); // Sembunyikan kuis
            Time.timeScale = 1f;            // Jalankan kembali waktu game
            
            // 📢 PERINTAH BARU: Buka gembok misi setelah kuis ditutup!
            if (LevelObjectiveManager.Instance != null)
            {
                LevelObjectiveManager.Instance.AktifkanMisi();
            }
        }
    }
}