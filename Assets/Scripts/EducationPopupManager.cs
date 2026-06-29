using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; 

public class EducationPopupManager : MonoBehaviour
{
    public static EducationPopupManager Instance { get; private set; }

    [Header("UI Elements (Education)")]
    public GameObject popupPanel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public Image popupImage; 
    public Button tombolLanjut;          // Tombol yang teksnya diganti jadi "Dapatkan Certificate Reward"

    [Header("UI Elements (Certificate Reward)")]
    public GameObject certificatePanel;   // Slot Baru: Seret objek 'PanelCertificate' ke sini
    public Button tombolSelesaiGame;     // Slot Baru: Seret objek 'TombolSelesaiGame' dari PanelCertificate ke sini

    [Header("Level Transition Slot")]
    public string namaSceneSelanjutnya; // Ketik nama scene start screen kamu (misal: StartScreen atau MainMenu)

    private bool isPopupActive = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning($"[Popup Manager] Menghapus komponen script duplikat pada {gameObject.name}, objek visual aman.");
            Destroy(this);
            return;
        }

        // Sembunyikan kedua panel di awal game
        if (popupPanel != null) popupPanel.SetActive(false);
        if (certificatePanel != null) certificatePanel.SetActive(false);
    }

    private void Start()
    {
        // Pasang fungsi klik ke tombol Dapatkan Certificate
        if (tombolLanjut != null)
        {
            tombolLanjut.onClick.RemoveAllListeners();
            tombolLanjut.onClick.AddListener(BukaPopupCertificate);
        }

        // Pasang fungsi klik ke tombol di dalam sertifikat untuk balik ke Start Screen
        if (tombolSelesaiGame != null)
        {
            tombolSelesaiGame.onClick.RemoveAllListeners();
            tombolSelesaiGame.onClick.AddListener(KembaliKeStartScreen);
        }
    }

    public void TampilkanPopup(PopupData data)
    {
        if (data == null)
        {
            Debug.LogError("[Popup Manager] Data Popup kosong / null!");
            return;
        }

        if (isPopupActive) return;
        isPopupActive = true;

        if (titleText != null) titleText.text = data.judulPopup; 
        if (descriptionText != null) descriptionText.text = data.isiPenjelasan; 
        if (popupImage != null && data.iconLevel != null) popupImage.sprite = data.iconLevel; 

        if (popupPanel != null) popupPanel.SetActive(true);

        Time.timeScale = 0f; // Freeze game
    }

    // ALUR BARU STEP 1: Menutup popup edukasi dan membuka popup sertifikat
    public void BukaPopupCertificate()
    {
        if (popupPanel != null) popupPanel.SetActive(false); // Tutup panel edukasi

        if (certificatePanel != null)
        {
            certificatePanel.SetActive(true); // Buka panel sertifikat
            Debug.Log("[Popup Manager] Popup Certificate Reward ditampilkan.");
        }
        else
        {
            Debug.LogError("[Popup Manager] Object PanelCertificate belum diseret ke Inspector!");
            // Jika lupa pasang panel, langsung lempar ke scene selanjutnya agar game tidak soft-lock
            KembaliKeStartScreen(); 
        }
    }

    // ALUR BARU STEP 2: Dipanggil saat tombol di sertifikat diklik
    public void KembaliKeStartScreen()
    {
        Time.timeScale = 1f; // Unfreeze waktu game

        if (!string.IsNullOrEmpty(namaSceneSelanjutnya))
        {
            Debug.Log($"[Popup Manager] Memuat scene Start Screen: {namaSceneSelanjutnya}");
            SceneManager.LoadScene(namaSceneSelanjutnya);
        }
        else
        {
            Debug.LogError("[Popup Manager] Nama scene Start Screen belum diisi di Inspector!");
        }
    }

    public void TutupPopup()
    {
        if (popupPanel != null) popupPanel.SetActive(false);
        if (certificatePanel != null) certificatePanel.SetActive(false);
        
        isPopupActive = false;
        Time.timeScale = 1f; 
    }
}