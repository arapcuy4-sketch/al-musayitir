using UnityEngine;

public class WrongItem : MonoBehaviour
{
    [SerializeField] private AudioClip suaraSalah;
    private HealthManager healthManager;

    void Start()
    {
        // Mencari objek HealthManager di scene
        healthManager = FindObjectOfType<HealthManager>();
    }

    private void OnMouseDown()
    {
        if (suaraSalah != null)
            AudioSource.PlayClipAtPoint(suaraSalah, transform.position);

        if (healthManager != null)
        {
            healthManager.KurangiNyawa();
        }
    }
}