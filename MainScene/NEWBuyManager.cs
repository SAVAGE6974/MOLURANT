using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NEWBuyManager : MonoBehaviour
{
    public Text credits_text;
    public static int credits = 800;
    
    public Text AL_1S_mainTextC; public static bool canUseSkillC = false; private bool canBuySkillC = true;
    public Text AL_1S_mainTextQ; public static bool canUseSkillQ = false; private bool canBuySkillQ = true;
    public Text AL_1S_mainTextX; public static bool canUseSkillX = false; private bool canBuySkillX = true;

    public Text Wakamo_mainTextC; public static bool Wakamo_canUseSkillC = false; private bool Wakamo_canBuySkillC = true;
    public Text Wakamo_mainTextX; public static bool Wakamo_canUseSkillX = false; private bool Wakamo_canBuySkillX = true;
    
    private void Awake()
    {
        credits_text.text = credits.ToString();
    }

    public void Wakamo_buySkillC()
    {
        if (Wakamo_canBuySkillC)
        {
            Wakamo_mainTextC.color = Color.white;
            
            Wakamo_canUseSkillC = true;
            Wakamo_canBuySkillC = false;
        }
    }

    public void buyHP()
    {
        NEWUI._hp = 100f;
    }

    // public void AL1S_buyBullet()
    // {
    //     // 정하기
    // }
    
    public void AL1S_buy_C()
    {
        if (credits >= 300 && canBuySkillC)
        {
            credits -= 300;
            credits_text.text = credits.ToString();
            
            AL_1S_mainTextC.color = Color.white;
            canUseSkillC = true;
            canBuySkillC = false;
        }
    }
    public void AL_1S_buy_Q()
    {
        AL_1S_mainTextQ.color = Color.white;
        canUseSkillQ = true;
    }

    public void AL_1S_buy_X()
    {
        AL_1S_mainTextX.color = Color.white;
        canUseSkillX = true;
    }
    
    public void Wakamo_buy_X()
    {
        Wakamo_mainTextX.color = Color.white;
        Wakamo_canUseSkillX = true;
    }
}
