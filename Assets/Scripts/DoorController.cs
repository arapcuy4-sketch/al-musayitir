using UnityEngine;

// Tempelkan ke objek Pintu. Bisa pakai Animator ATAU pergerakan manual sederhana di bawah.
public class DoorController : MonoBehaviour
{
    [Header("Opsi 1: Pakai Animator (disarankan)")]
    public Animator animatorPintu;
    public string namaTriggerAnimasi = "Open";

    [Header("Opsi 2: Gerakkan manual tanpa Animator")]
    public bool gunakanGerakManual = false;
    public Vector3 posisiTerbuka; // set posisi target saat pintu terbuka (misal naik ke atas)
    public float kecepatanGerak = 2f;

    private bool sedangTerbuka = false;
    private Vector3 posisiAwal;

    void Start()
    {
        posisiAwal = transform.position;
    }

    public void BukaPintu()
    {
        sedangTerbuka = true;

        if (animatorPintu != null)
        {
            animatorPintu.SetTrigger(namaTriggerAnimasi);
        }

        // Jika pakai gerak manual, Update() di bawah akan menangani pergerakannya
    }

    void Update()
    {
        if (sedangTerbuka && gunakanGerakManual)
        {
            transform.position = Vector3.MoveTowards(transform.position, posisiAwal + posisiTerbuka, kecepatanGerak * Time.deltaTime);
        }
    }
}
