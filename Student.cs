using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Student : MonoBehaviour
{
    public GameObject AL1SUpgradeButton;
    public GameObject WakamoUpgradeButton;
    
    private void Awake()
    {
        AL1SUpgradeButton.SetActive(false);
        WakamoUpgradeButton.SetActive(false);
    }

    private void Update()
    {
        cheakWhoIsLast();
    }

    private void cheakWhoIsLast()
    {
        if (ST_PanelController.LastSelectedStudent == "AL1S")
            AL1SUpgradeButton.SetActive(true);
        else if (ST_PanelController.LastSelectedStudent != "AL1S")
            WakamoUpgradeButton.SetActive(true);
    }
}
