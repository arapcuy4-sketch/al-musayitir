using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // DIWajibkan untuk menggunakan Coroutine (jeda waktu)

public class SceneLoader : MonoBehaviour
{
    // Tempat menaruh komponen suara klik di Inspector
    public AudioSource clickSFX; 

    public void LoadGame()
    {
        // Menjalankan fungsi pindah scene dengan jeda waktu
        StartCoroutine(PlaySoundAndLoad());
    }

    IEnumerator PlaySoundAndLoad()
    {
        if (clickSFX != null)
        {
            clickSFX.Play(); // Mainkan suara klik
            
            // Tunggu sampai durasi efek suara kliknya selesai sebelum pindah layar
            yield return new WaitForSeconds(clickSFX.clip.length);
        }

        // Pindah ke MainMenu setelah suara selesai
        SceneManager.LoadScene("MainMenu");
    }
}