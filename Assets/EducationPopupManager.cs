using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EducationPopupManager : MonoBehaviour
{
    public static EducationPopupManager Instance { get; private set; }

    [Header("Referensi UI Panel Akhir (Popup Edukasi)")]
    public GameObject panelPopupEdukasi; 
    public TextMeshProUGUI textJudul;
    public TextMeshProUGUI textIsi;
    public TextMeshProUGUI textTombol;
    
    [Header("PERINGATAN: Masukkan 'TombolLanjutLevel' dari PanelPopup, BUKAN Tombol Quiz!")]
    public Button tombolSelesaiDanPindahLevel; // Nama variabel diganti total agar reset di Inspector
    public Image imageIcon;

    private void Awake()
    {
        Instance = this;

        if (panelPopupEdukasi != null)
            panelPopupEdukasi.SetActive(false);

        // Memasang fungsi klik secara otomatis ke tombol yang benar
        if (tombolSelesaiDanPindahLevel != null)
        {
            tombolSelesaiDanPindahLevel.onClick.RemoveAllListeners(); // Bersihkan sisa referensi lama
            tombolSelesaiDanPindahLevel.onClick.AddListener(OnTombolLanjutDiklik);
        }
    }

    public void TampilkanPopup(PopupData data)
    {
        Time.timeScale = 0f;

        if (textJudul != null) textJudul.text = data.judulPopup;
        if (textIsi != null) textIsi.text = data.isiPenjelasan;
        if (textTombol != null) textTombol.text = data.teksTombol;
        if (imageIcon != null) imageIcon.sprite = data.iconLevel;

        if (panelPopupEdukasi != null)
            panelPopupEdukasi.SetActive(true);

        Debug.Log("[System] Popup Edukasi Muncul! Game di-freeze.");
    }

    private void OnTombolLanjutDiklik()
    {
        // Debug ini untuk membuktikan kalau tombol ini yang diklik
        Debug.Log("[System] Tombol 'Lanjutkan Level' sukses diklik. Memuat Scene Berikutnya...");

        if (panelPopupEdukasi != null)
            panelPopupEdukasi.SetActive(false);

        Time.timeScale = 1f;

        // Pindah ke level berikutnya
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}