using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class MisiData
{
    public string itemGroupId;
    public int jumlahDibutuhkan;
    public int jumlahSekarang = 0;
}

public class LevelObjectiveManager : MonoBehaviour
{
    public static LevelObjectiveManager Instance { get; private set; }

    [Header("Daftar Misi")]
    public List<MisiData> daftarMisi = new List<MisiData>();

    [Header("UI Progress")]
    public TextMeshProUGUI vehicleText;
    public TextMeshProUGUI animalText;

    [Header("Popup Edukasi")]
    public PopupData popupEdukasi;

    private bool sudahPicuWin = false;
    private bool misiBolehDimulai = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AktifkanMisi()
    {
        misiBolehDimulai = true;
        Debug.Log("[System] Misi dimulai!");
    }

    private void Update()
    {
        if (!misiBolehDimulai || sudahPicuWin)
            return;

        CekKondisiMisi();
    }

    private void CekKondisiMisi()
    {
        bool semuaMisiSelesai = true;

        foreach (var misi in daftarMisi)
        {
            if (misi.jumlahSekarang < misi.jumlahDibutuhkan)
            {
                semuaMisiSelesai = false;
                break;
            }
        }

        if (semuaMisiSelesai && daftarMisi.Count > 0)
        {
            sudahPicuWin = true;

            Debug.Log("[WIN] Semua objek ditemukan!");

            if (EducationPopupManager.Instance != null)
            {
                EducationPopupManager.Instance.TampilkanPopup(popupEdukasi);
            }
        }
    }

    public void ItemDitemukan(string id)
    {
        TambahBarang(id, 1);
    }

    public void ItemDitemukan(string id, int jumlah)
    {
        TambahBarang(id, jumlah);
    }

    private void TambahBarang(string id, int jumlah)
    {
        Debug.Log("[CLICK] Objek ditemukan : " + id);

        foreach (var misi in daftarMisi)
        {
            if (misi.itemGroupId == id)
            {
                misi.jumlahSekarang += jumlah;

                Debug.Log(
                    $"[PROGRESS] {misi.itemGroupId} : {misi.jumlahSekarang}/{misi.jumlahDibutuhkan}"
                );
            }
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        foreach (var misi in daftarMisi)
        {
            if (misi.itemGroupId == "Vehicle")
            {
                if (vehicleText != null)
                {
                    vehicleText.text =
                        $"Vehicle Found : {misi.jumlahSekarang}/{misi.jumlahDibutuhkan}";
                }
            }

            if (misi.itemGroupId == "Animal")
            {
                if (animalText != null)
                {
                    animalText.text =
                        $"Animal Found : {misi.jumlahSekarang}/{misi.jumlahDibutuhkan}";
                }
            }
        }
    }
}