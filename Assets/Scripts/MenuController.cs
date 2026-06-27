using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuController : MonoBehaviour
{
    public GameObject panelLevelSelection;
    
    // 1. TAMBAHKAN VARIABEL BARU UNTUK PANEL SETTINGS
    public GameObject panelSettings; 

    // ========================================================
    // FUNGSI UNTUK PANEL LEVEL SELECTION
    // ========================================================
    public void BukaLevelSelection()
    {
        panelLevelSelection.SetActive(true);
    }

    public void TutupLevelSelection()
    {
        panelLevelSelection.SetActive(false);
    }

    // ========================================================
    // FUNGSI BARU UNTUK PANEL SETTINGS
    // ========================================================
    public void BukaSettings()
    {
        panelSettings.SetActive(true);
    }

    public void TutupSettings()
    {
        panelSettings.SetActive(false);
    }

    // ========================================================
    // FUNGSI PINDAH SCENE
    // ========================================================
    public void PindahKeLevel1()
    {
        SceneManager.LoadScene("Level 1"); 
    }

    public void PindahKeLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void PindahKeLevel3()
    {
        SceneManager.LoadScene("Level 3");
    }

    public void KembaliKeStartScreen()
    {
        SceneManager.LoadScene("Start Screen");
    }
}