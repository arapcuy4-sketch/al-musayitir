using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio; // Tambahan untuk mengatur AudioMixer
using UnityEngine.UI;    // Tambahan untuk elemen UI seperti Slider
using TMPro;           // Tambahan untuk TextMeshPro (Dropdown)

public class MenuController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject titleScreenPanel; // Panel untuk Judul
    public GameObject mainMenuPanel;    // Panel untuk Main Menu (Play, Settings, dll)
    public GameObject levelSelectionPanel; // Panel untuk Pilih Level
    public GameObject settingsPanel;    // TAMBAHAN: Panel untuk Settings

    [Header("Settings - Audio & Language")] // TAMBAHAN: Referensi UI Settings
    public AudioMixer mainMixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    public TMP_Dropdown languageDropdown;

    private void Start()
    {
        // Saat game pertama kali mulai, HANYA tampilkan Title Screen
        ShowTitleScreen();

        // --- TAMBAHAN UNTUK INISIALISASI SETTINGS ---
        // Memastikan panel settings tertutup saat awal mulai
        if (settingsPanel != null) settingsPanel.SetActive(false);

        // Memuat data pengaturan yang sudah disimpan sebelumnya (atau nilai default)
        if (musicSlider != null && sfxSlider != null && languageDropdown != null)
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
            languageDropdown.value = PlayerPrefs.GetInt("LanguagePreference", 0);

            // Terapkan pengaturan agar langsung aktif saat game dimulai
            SetMusicVolume(musicSlider.value);
            SetSFXVolume(sfxSlider.value);
            SetLanguage(languageDropdown.value);
        }
    }

    // --- FUNGSI UNTUK TITLE SCREEN ---
    public void ShowTitleScreen()
    {
        titleScreenPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        levelSelectionPanel.SetActive(false);
        
        // Tambahan: Pastikan settings juga tertutup jika kembali ke Title
        if (settingsPanel != null) settingsPanel.SetActive(false); 
    }

    // Fungsi ini dipanggil saat Judul di-klik
    public void EnterMainMenu()
    {
        titleScreenPanel.SetActive(false); // Sembunyikan judul
        mainMenuPanel.SetActive(true);     // Tampilkan menu utama
    }

    // --- FUNGSI UNTUK MAIN MENU ---
    public void PlayGame()
    {
        // Ganti "Level1" dengan nama scene game utama Anda jika sudah ada
        SceneManager.LoadScene("Level1"); 
    }

    public void ShowLevelSelection()
    {
        mainMenuPanel.SetActive(false);
        levelSelectionPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Debug.Log("Game Exited!"); 
        Application.Quit(); 
    }

    // --- FUNGSI UNTUK TOMBOL "BACK" ---
    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        levelSelectionPanel.SetActive(false);
        
        // Tambahan: Pastikan settings tertutup saat kembali ke Main Menu
        if (settingsPanel != null) settingsPanel.SetActive(false);
    }

    // --- Tambahan untuk menyambungkan button Level selection ke scene

    public void LoadLevel(string LevelName)
    {
        SceneManager.LoadScene(LevelName);
    }


    // ====================================================================
    // --- TAMBAHAN KHUSUS UNTUK PANEL SETTINGS ---------------------------
    // ====================================================================

    // Dipanggil saat tombol Settings di Main Menu diklik
    public void OpenSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    // Dipanggil saat tombol Back/Close di dalam panel Settings diklik
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    // Dipanggil oleh MusicSlider (On Value Changed)
    public void SetMusicVolume(float volume)
    {
        if (mainMixer == null) return;

        // Mencegah error logaritma jika volume 0, dan konversi ke nilai Desibel
        float dbVolume = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20;
        if (volume == 0) dbVolume = -80f; // Mute total
        
        mainMixer.SetFloat("MusicVol", dbVolume);
        PlayerPrefs.SetFloat("MusicVolume", volume); // Simpan pengaturan
    }

    // Dipanggil oleh SFXSlider (On Value Changed)
    public void SetSFXVolume(float volume)
    {
        if (mainMixer == null) return;

        float dbVolume = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20;
        if (volume == 0) dbVolume = -80f;
        
        mainMixer.SetFloat("SFXVol", dbVolume);
        PlayerPrefs.SetFloat("SFXVolume", volume); // Simpan pengaturan
    }

    // Dipanggil oleh LanguageDropdown (On Value Changed)
    public void SetLanguage(int languageIndex)
    {
        PlayerPrefs.SetInt("LanguagePreference", languageIndex);
        
        if (languageIndex == 0)
        {
            Debug.Log("Bahasa diubah ke: English");
            // Logika ganti teks ke bahasa Inggris nantinya bisa ditaruh di sini
        }
        else if (languageIndex == 1)
        {
            Debug.Log("Bahasa diubah ke: Bahasa Indonesia");
            // Logika ganti teks ke bahasa Indonesia nantinya bisa ditaruh di sini
        }
    }
}