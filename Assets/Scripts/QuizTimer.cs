using UnityEngine;
using TMPro;

public class QuizTimer : MonoBehaviour
{
    [Header("Pengaturan")]
    public float totalWaktu = 60f; 
    private float sisaWaktu;
    private bool timerBerjalan = false;

    public TextMeshProUGUI textWaktu;
    public GameObject panelQuizAwal;

    void Start()
    {
        // Jangan langsung jalan di Start jika ingin menunggu pemain
        // MulaiUlangTimer(); 
    }

    void Update()
    {
        if (timerBerjalan)
        {
            if (sisaWaktu > 0)
            {
                sisaWaktu -= Time.deltaTime;
                UpdateTampilanWaktu(sisaWaktu);
            }
            else
            {
                sisaWaktu = 0;
                timerBerjalan = false;
                WaktuHabis();
            }
        }
    }

    void UpdateTampilanWaktu(float waktu)
    {
        float menit = Mathf.FloorToInt(waktu / 60);
        float detik = Mathf.FloorToInt(waktu % 60);
        textWaktu.text = string.Format("{0:00}:{1:00}", menit, detik);
    }

    public void WaktuHabis()
    {
        timerBerjalan = false;
        panelQuizAwal.SetActive(true);
    }

    // PANGGIL FUNGSI INI KETIKA TOMBOL "LANJUT" DIKLIK ATAU KUIS DIMULAI
    public void ResetDanJalankanTimer()
    {
        sisaWaktu = totalWaktu;
        timerBerjalan = true;
    }
}