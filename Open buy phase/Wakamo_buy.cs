using System;
using UnityEngine;

public class Open_buy_phase_Wakamo : MonoBehaviour
{
    [SerializeField] private BuyPhase buyPhase;
    [SerializeField] private NEWGamemenager gamemanager;
    
    public GameObject buyUIPanel;
    
    public static bool Wakamo_isOpen;
    private string _gunName;

    private Player playerScript;

    private void Awake()
    {
        _gunName = SuperNova.gunName;
        playerScript = FindObjectOfType<Player>();
        buyUIPanel.SetActive(false);
    }

    private void Start()
    {
        if (_gunName != "Wakamo")
        {
            buyUIPanel.SetActive(false);
            this.enabled = false; // ✅ 비활성화
        }
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.B) && _gunName == "Wakamo" && !gamemanager.isMainPhase)
    //     {
    //         if (Wakamo_isOpen)
    //         {
    //             ForceCloseBuyUI();
    //         }
    //         else
    //         {
    //             buyUIPanel.SetActive(true);
    //             Wakamo_isOpen = true;
    //             buyPhase.HideBuyPhase();
    //
    //             Cursor.lockState = CursorLockMode.Confined;
    //             Cursor.visible = true;
    //
    //             if (playerScript != null)
    //                 playerScript.isMouseLookEnabled = false;
    //         }
    //     }
    // }

    public void ForceCloseBuyUI()
    {
        buyUIPanel.SetActive(false);
        Wakamo_isOpen = false;
        buyPhase.ShowBuyPhaseText();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerScript != null)
            playerScript.isMouseLookEnabled = true;
    }
}