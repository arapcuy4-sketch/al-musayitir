using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI vehicleText;
    public TextMeshProUGUI animalText;

    int vehicleFound = 0;
    int animalFound = 0;

    public void ObjectFound(string type)
    {
        if (type == "Vehicle")
        {
            vehicleFound++;
            vehicleText.text = "Vehicle Found : " + vehicleFound + "/4";
        }
        else if (type == "Animal")
        {
            animalFound++;
            animalText.text = "Animal Found : " + animalFound + "/4";
        }
    }
}