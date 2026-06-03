using UnityEngine;
using UnityEngine.SceneManagement; // Tambahkan ini agar bisa pindah scene

public class MenuController : MonoBehaviour
{
    public GameObject panelLevelSelection;

    // Fungsi untuk membuka panel (sudah ada)
    public void BukaLevelSelection()
    {
        panelLevelSelection.SetActive(true);
    }

    // FUNGSI BARU: Untuk menutup panel
    public void TutupLevelSelection()
    {
        panelLevelSelection.SetActive(false);
    }

    // FUNGSI BARU: Untuk pindah ke scene Level 1
   public void PindahKeLevel1()
{
    // Menggunakan tanda kutip dua untuk nama scene "Level 1"
    SceneManager.LoadScene("Level 1"); 
}
}