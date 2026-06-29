using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthManager : MonoBehaviour
{
    [Header("Sistem Nyawa")]
    public int nyawa = 3;
    public GameObject[] ikonHati;

    [Header("Pengaturan Game Over (Reset)")]
    public GameObject panelKuisAwal; 
    public GameObject panelGameplay; 
    public TMP_Text teksPercobaan;   

    private int jumlahPercobaan = 1; 

    // PANGGIL FUNGSI INI DI TOMBOL "MULAI" ATAU "PLAY"
    public void MulaiPermainan()
    {
        // 1. Reset nyawa ke penuh
        nyawa = 3;
        UpdateTampilanHati();

        // 2. Sembunyikan menu awal, tampilkan gameplay
        if (panelKuisAwal != null) panelKuisAwal.SetActive(false);
        if (panelGameplay != null) panelGameplay.SetActive(true);
    }

    public void KurangiNyawa()
    {
        if (nyawa > 0)
        {
            nyawa--;
            UpdateTampilanHati();

            if (nyawa <= 0)
            {
                TriggerGameOver();
            }
        }
    }

    void UpdateTampilanHati()
    {
        for (int i = 0; i < ikonHati.Length; i++)
        {
            // Menyalakan hati jika indeksnya lebih kecil dari sisa nyawa
            ikonHati[i].SetActive(i < nyawa);
        }
    }

    void TriggerGameOver()
    {
        // 1. Tambah hitungan percobaan
        jumlahPercobaan++;
        if (teksPercobaan != null)
        {
            teksPercobaan.text = "Percobaan ke: " + jumlahPercobaan;
        }

        // 2. Kembalikan ke layar awal
        if (panelGameplay != null) panelGameplay.SetActive(false);
        if (panelKuisAwal != null) panelKuisAwal.SetActive(true);
    }
}