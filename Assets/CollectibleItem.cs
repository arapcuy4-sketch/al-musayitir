using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [Tooltip("Harus sama dengan itemGroupId di LevelObjectiveManager")]
    public string itemGroupId;

    private bool sudahDiambil = false;

    private void OnMouseDown()
    {
        if (sudahDiambil)
            return;

        sudahDiambil = true;

        if (LevelObjectiveManager.Instance != null)
        {
            LevelObjectiveManager.Instance.ItemDitemukan(itemGroupId);
        }

        gameObject.SetActive(false);
    }
}