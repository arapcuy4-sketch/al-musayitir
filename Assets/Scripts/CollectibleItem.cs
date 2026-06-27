using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [Header("Pengaturan Item")]
    [Tooltip("Harus sama dengan itemGroupId di LevelObjectiveManager")]
    public string itemGroupId;

    [Header("Pengaturan Audio")]
    [SerializeField] private AudioClip suaraCollect; 

    private bool sudahDiambil = false;

    private void OnMouseDown()
    {
        // Mencegah klik ganda jika sudah diambil
        if (sudahDiambil)
            return;

        sudahDiambil = true;

        // Memainkan suara di posisi item (tidak terpotong saat item hilang)
        if (suaraCollect != null)
        {
            AudioSource.PlayClipAtPoint(suaraCollect, transform.position);
        }

        // Memberitahu Manager bahwa item ditemukan
        if (LevelObjectiveManager.Instance != null)
        {
            LevelObjectiveManager.Instance.ItemDitemukan(itemGroupId);
        }

        // Menyembunyikan objek
        gameObject.SetActive(false);
    }
}