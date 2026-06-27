using UnityEngine;
using UnityEngine.UI; // Wajib untuk mengakses komponen Image

public class HealthManager : MonoBehaviour
{
    public int nyawa = 3; // Jumlah nyawa awal
    public GameObject[] ikonHati; // Masukkan objek gambar hati ke sini di Inspector

    public void KurangiNyawa()
    {
        if (nyawa > 0)
        {
            nyawa--;
            UpdateTampilanHati();

            if (nyawa <= 0)
            {
                Debug.Log("Game Over!");
                // Panggil fungsi Game Over atau restart scene di sini
            }
        }
    }

    void UpdateTampilanHati()
    {
        for (int i = 0; i < ikonHati.Length; i++)
        {
            // Jika indeks i lebih kecil dari jumlah nyawa, hati terlihat (aktif)
            // Jika tidak, hati disembunyikan
            ikonHati[i].SetActive(i < nyawa);
        }
    }
}