using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiButtonWakamoManager : MonoBehaviour
{
    public GameObject cheakWindow_Wakamo;
    
    void Awake()
    {
        cheakWindow_Wakamo.SetActive(false);
    }

    public void showCheakWindow()
    {
        cheakWindow_Wakamo.SetActive(true);
    }

    public void hideCheakWindow()
    {
        cheakWindow_Wakamo.SetActive(false);
    }
}
