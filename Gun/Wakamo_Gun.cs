using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wakamo_gun : MonoBehaviour
{
    private Vector3 _originalPosition;
    public float shakeAmount = 0.05f;
    public float shakeSpeed = 20f;

    public GameObject destorySidebarPannel;

    public static string gunName;
    
    private void Awake()
    {
        _originalPosition = transform.localPosition;
        gunName = LockinManager.lastSelectedCharacter;
    }

    
    private void Start()
    {
        if (gunName != "Wakamo")
        {
            Destroy(this.gameObject);
            // Destroy(this.destorySidebarPannel);
            destorySidebarPannel.SetActive(false);
        }
        
    }
}
