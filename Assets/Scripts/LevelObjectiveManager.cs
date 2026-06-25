using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Diperlukan untuk TextMeshPro

public class LevelObjectiveManager : MonoBehaviour
{
    // --- SINGLETON SYSTEM (Menghilangkan Error CS0117) ---
    public static LevelObjectiveManager Instance { get; private set; }

    [System.Serializable]
    public class MisiData
    {
        public string itemGroupId;
        public int jumlahDibutuhkan;
        public int jumlahSekarang;
    }

    [Header("Daftar Misi")]
    public List<MisiData> daftarMisi = new List<MisiData>();

    [Header("UI Progress")]
    public TextMeshProUGUI vehicleText;
    public TextMeshProUGUI animalText;

    [Header("Popup Edukasi")]
    public PopupData popupEdukasi; // Sesuaikan nama Class ScriptableObject kelompokmu jika berbeda

    private bool isLevelSelesai = false;
    private bool isMisiAktif = false; // Status apakah kuis awal sudah selesai dan misi dimulai

    private void Awake()
    {
        // Inisialisasi Singleton agar script lain bisa memanggil via .Instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Jalankan update text di awal game jika diperlukan
        UpdateUITeks();
    }

    void Update()
    {
        // Hanya cek kondisi kelulusan jika misi sudah diaktifkan (kuis awal selesai)
        if (isMisiAktif)
        {
            CekKondisiMisi();
        }
    }

    // --- TAMBAHAN FUNGSI UNTUK MENGHILANGKAN ERROR CS1061 ---
    public void AktifkanMisi()
    {
        isMisiAktif = true;
        Debug.Log("[SYSTEM] Kuis awal selesai, misi pencarian barang sekarang AKTIF!");
    }

    // Fungsi utama yang diakses oleh CollectibleItem.cs
    public void ItemDitemukan(string groupId)
    {
        if (isLevelSelesai || !isMisiAktif) return;

        TambahBarang(groupId, 1);
    }

    public void TambahBarang(string groupId, int jumlah)
    {
        foreach (var misi in daftarMisi)
        {
            if (misi.itemGroupId == groupId)
            {
                misi.jumlahSekarang += jumlah;
                Debug.Log($"[CLICK] Objek ditemukan : {groupId}");
                Debug.Log($"[PROGRESS] {groupId} : {misi.jumlahSekarang}/{misi.jumlahDibutuhkan}");
                
                // Update teks visual setelah data bertambah
                UpdateUITeks();
                break;
            }
        }
    }

    private void CekKondisiMisi()
    {
        if (isLevelSelesai) return;

        bool semuaMisiSelesai = true;

        foreach (var misi in daftarMisi)
        {
            if (misi.jumlahSekarang < misi.jumlahDibutuhkan)
            {
                semuaMisiSelesai = false;
                break;
            }
        }

        // Jika semua barang yang dicari sudah terpenuhi nilainya
        if (semuaMisiSelesai && daftarMisi.Count > 0)
        {
            isLevelSelesai = true;
            Debug.Log("[WIN] Semua objek ditemukan! Menuju End Game.");
            TriggerEndGame();
        }
    }

    private void UpdateUITeks()
    {
        // AMAN: Code hanya dieksekusi jika slot Text tidak kosong (None)
        foreach (var misi in daftarMisi)
        {
            if (misi.itemGroupId == "Vehicle" && vehicleText != null)
            {
                vehicleText.text = $"Vehicle: {misi.jumlahSekarang}/{misi.jumlahDibutuhkan}";
            }
            else if (misi.itemGroupId == "Animal" && animalText != null)
            {
                animalText.text = $"Animal: {misi.jumlahSekarang}/{misi.jumlahDibutuhkan}";
            }
        }
    }

    private void TriggerEndGame()
    {
        // Memanggil EducationPopupManager untuk memicu pembukaan Popup Edukasi
        if (EducationPopupManager.Instance != null)
        {
            Debug.Log("[SYSTEM] Memicu Popup Edukasi ke Layar. Game Freeze.");
            EducationPopupManager.Instance.TampilkanPopup(popupEdukasi);
        }
        else
        {
            Debug.LogError("[ERROR] EducationPopupManager tidak ditemukan di Scene ini! Cek kembali Hierarchy kamu.");
        }
    }
}