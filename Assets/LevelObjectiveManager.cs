using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MisiData
{
    public string itemGroupId;
    public int jumlahDibutuhkan;
    public int jumlahSekarang = 0; // Diubah ke public agar bisa kamu pantau di Inspector saat game jalan
}

public class LevelObjectiveManager : MonoBehaviour
{
    public static LevelObjectiveManager Instance { get; private set; }

    [Header("Daftar Misi")]
    public List<MisiData> daftarMisi = new List<MisiData>();

    [Header("Popup Edukasi")]
    public PopupData popupEdukasi;

    private bool sudahPicuWin = false; 
    private bool misiBolehDimulai = false; // 🔒 KUNCI UTAMA: Misi digembok di awal game

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // 📢 Fungsi ini akan dipanggil oleh QuizStartManager saat tombol kuis diklik
    public void AktifkanMisi()
    {
        misiBolehDimulai = true;
        Debug.Log("[System] Kuis ditutup. Misi berburu item SEKARANG RESMI DIMULAI!");
    }

    void Update()
    {
        // JIKA kuis belum selesai ATAU game sudah menang, JANGAN CEK APAPAUN!
        if (!misiBolehDimulai || sudahPicuWin) return;

        CekKondisiMisi();
    }

    private void CekKondisiMisi()
    {
        bool semuaMisiSelesai = true;

        foreach (var misi in daftarMisi)
        {
            if (misi.jumlahSekarang < misi.jumlahDibutuhkan || misi.jumlahDibutuhkan <= 0)
            {
                semuaMisiSelesai = false;
                break; 
            }
        }

        if (semuaMisiSelesai && daftarMisi.Count > 0)
        {
            sudahPicuWin = true; 
            Debug.Log("[System] Misi Selesai! Memunculkan Catatan Penelitian.");
            
            if (EducationPopupManager.Instance != null)
            {
                EducationPopupManager.Instance.TampilkanPopup(popupEdukasi);
            }
        }
    }

    public void ItemDitemukan(string id, int jumlah) => TambahBarang(id, jumlah);
    public void ItemDitemukan(string id) => TambahBarang(id, 1);

    public void TambahBarang(string id, int jumlah)
    {
        foreach (var misi in daftarMisi)
        {
            if (misi.itemGroupId == id)
            {
                misi.jumlahSekarang += jumlah;
                Debug.Log($"[Item] {id} bertambah! Sekarang: {misi.jumlahSekarang}/{misi.jumlahDibutuhkan}");
            }
        }
    }
}