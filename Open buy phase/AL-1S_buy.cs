using System;
using UnityEngine;

public class Open_buy_phase_AL1S : MonoBehaviour
{
    [SerializeField] private BuyPhase buyPhase;
    [SerializeField] private NEWGamemenager gamemanager;
    
    public GameObject buyUIPanel;

    public static bool AL1S_isOpen;
    private string _gunName;
    
    private Player playerScript;

    private void Awake()
    {
        _gunName = LockinManager.lastSelectedCharacter;
        playerScript = FindObjectOfType<Player>();
        buyUIPanel.SetActive(false);
    }

    private void Start()
    {
        // ✅ AL-1S가 아니면 이 스크립트 꺼버림 + UI도 꺼버림
        if (_gunName != "AL-1S")
        {
            buyUIPanel.SetActive(false);
            this.enabled = false;
        }
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.B) && _gunName == "AL-1S" && !gamemanager.isMainPhase)
    //     {
    //         if (AL1S_isOpen)
    //         {
    //             ForceCloseBuyUI();
    //         }
    //         else
    //         {
    //             buyUIPanel.SetActive(true);
    //             AL1S_isOpen = true;
    //             buyPhase.HideBuyPhase();
    //
    //             Cursor.lockState = CursorLockMode.Confined;
    //             Cursor.visible = true;
    //
    //             if (playerScript != null)
    //                 playerScript.isMouseLookEnabled = false;
    //         }
    //     }
    //
    //     if (Gamemanager.cooldownTime == 0) // 카운트 다운이 끝나면 강제로 닫기
    //     {
    //         ForceCloseBuyUI();
    //     }
    // }

    public void ForceCloseBuyUI()
    {
        buyUIPanel.SetActive(false);
        AL1S_isOpen = false;
        buyPhase.ShowBuyPhaseText();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerScript != null)
            playerScript.isMouseLookEnabled = true;
    }
}