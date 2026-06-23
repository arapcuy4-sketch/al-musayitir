using UnityEngine;

// Klik kanan di Project window -> Create -> Lab OOP -> Popup Edukasi
// Buat 1 asset PopupData untuk setiap level (Level1_ClassObject, Level2_Inheritance, Level3_Interface)
[CreateAssetMenu(fileName = "NewPopupData", menuName = "Lab OOP/Popup Edukasi")]
public class PopupData : ScriptableObject
{
    [Header("Identitas Popup")]
    public string judulPopup;

    [TextArea(8, 20)]
    public string isiPenjelasan;

    public string teksTombol = "Lanjutkan";

    [Header("Opsional - Visual")]
    public Sprite iconLevel; // ikon kecil di pojok popup (opsional)
}
