using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // Diperlukan untuk sistem pindah level

public class EducationPopupManager : MonoBehaviour
{
    public static EducationPopupManager Instance { get; private set; }

    [Header("UI Elements")]
    public GameObject popupPanel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public Image popupImage; 

    [Header("Level Transition Slot")]
    public Button tombolLanjut;          // Slot untuk seret objek Tombol Lanjut dari Hierarchy
    public string namaSceneSelanjutnya; // Slot untuk ngetik nama scene (misal: Level 2)

    private bool isPopupActive = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
        }
    }

    private void Start()
    {
        // Otomatis memasang fungsi klik ke tombol yang kamu seret di Inspector
        if (tombolLanjut != null)
        {
            tombolLanjut.onClick.RemoveAllListeners();
            tombolLanjut.onClick.AddListener(LanjutKeLevelBerikutnya);
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

        if (titleText != null) 
            titleText.text = data.judulPopup; 

        if (descriptionText != null) 
            descriptionText.text = data.isiPenjelasan; 

        if (popupImage != null && data.iconLevel != null) 
            popupImage.sprite = data.iconLevel; 

        if (popupPanel != null)
        {
            popupPanel.SetActive(true);
        }

        Time.timeScale = 0f; // Menghentikan waktu game saat popup edukasi muncul
    }

    public void TutupPopup()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
        }
        
        isPopupActive = false;
        Time.timeScale = 1f; // Kembalikan waktu ke normal jika panel ditutup biasa
    }

    // Fungsi otomatis yang dijalankan saat tombolLanjut yang kamu seret itu diklik
    public void LanjutKeLevelBerikutnya()
    {
        Time.timeScale = 1f; // Normalisasi waktu game agar scene selanjutnya tidak freeze

        if (!string.IsNullOrEmpty(namaSceneSelanjutnya))
        {
            Debug.Log($"[Popup Manager] Memuat scene: {namaSceneSelanjutnya}");
            SceneManager.LoadScene(namaSceneSelanjutnya);
        }
        else
        {
            Debug.LogError("[Popup Manager] Nama scene selanjutnya belum diisi di Inspector!");
        }
    }
}