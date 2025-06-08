using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NEWGamemenager : MonoBehaviour
{
    public NEWSampleLaserClick AL_1S_laserClick;
    
    public Text cooltime_AL_1S_eskill;
    public Text cooltime_Wakamo_eskill;
    public Text AL_1S_mainTextE;
    public Text AL_1S_mainTextC;
    public Text AL_1S_mainTextQ;
    public Text AL_1S_mainTextX;
    
    public Text Wakamo_mainTextE;
    public Text Wakamo_mainTextC;
    public Text Wakamo_mainTextQ;
    public Text Wakamo_mainTextX;
    
    public static bool AL_1S_canUseSkillE;
    public static bool Wakamo_canUseSkillE;
    public static float AL_1S_cooldownTime = 20f;
    public static float Wakamo_cooldownTime = 30f;
    public static float currentCooldown = 0f;
    public static bool AL_1S_shootSkillX = false;
    public static bool Wakamo_useE = false;
    public static bool Wakamo_useC = false; // ✅ C 스킬 사용 여부

    public GameObject AL1Sgun;
    public GameObject Wakamogun;

    public Image targetUIImage;

    private void Awake()
    {
        AL1Sgun.SetActive(false);
        Wakamogun.SetActive(false);
        AL_1S_canUseSkillE = true;
        Wakamo_canUseSkillE = true;
    }

    private void Start()
    {
        if (LockinManager.lastSelectedCharacter == "AL-1S")
        {
            AL1Sgun.SetActive(true);
            Wakamogun.SetActive(false);

            AL_1S_mainTextC.color = Color.gray;
            AL_1S_mainTextQ.color = Color.gray;
            AL_1S_mainTextX.color = Color.gray;
        }
        else if (LockinManager.lastSelectedCharacter == "Wakamo")
        {
            AL1Sgun.SetActive(false);
            Wakamogun.SetActive(true);
            
            Wakamo_mainTextC.color = Color.gray;
            Wakamo_mainTextQ.color = Color.gray;
            Wakamo_mainTextX.color = Color.gray;
        }
    }

    private void Update()
    {
        AL_1S_CheckCooldownE();
        Wakamo_CheckCooldownE();
        AL1S_SkillC();
        AL1S_SkillQ();
        AL1S_SkillX();
        Wakamo_skillE();
        Wakamo_skillC(); // ✅ C 스킬 처리 추가
    }

    private void Wakamo_CheckCooldownE()
    {
        if (!Wakamo_canUseSkillE)
        {
            currentCooldown -= Time.deltaTime;
            cooltime_Wakamo_eskill.text = Mathf.Ceil(currentCooldown).ToString() + "s";
            Wakamo_mainTextE.color = Color.gray;

            if (currentCooldown <= 0f)
            {
                Wakamo_canUseSkillE = true;
                cooltime_Wakamo_eskill.text = "";
                Wakamo_mainTextE.color = Color.white;
            }
        }
        else
        {
            Wakamo_mainTextE.color = Color.white;
        }
    }

    public void Wakamo_skillE()
    {
        if (LockinManager.lastSelectedCharacter == "Wakamo" &&
            Input.GetKeyDown(KeyCode.E) &&
            Wakamo_mainTextE.color == Color.white)
        {
            Wakamo_canUseSkillE = false;
            currentCooldown = Wakamo_cooldownTime;
            Debug.Log("Wakamo E 키 사용! 30초 쿨타임 시작!");

            StartCoroutine(ChangeUIImageColorFor15Seconds());
        }
    }
    
    private void Wakamo_skillC()
    {
        if (LockinManager.lastSelectedCharacter == "Wakamo" &&
            Input.GetKeyDown(KeyCode.C) &&
            Wakamo_mainTextC.color == Color.white)
        {
            targetUIImage.color = Color.cyan;
            Wakamo_mainTextC.color = Color.gray;
            Wakamo_useC = true;
            StartCoroutine(WakamoC_Reset());
        }
    }

    private IEnumerator WakamoC_Reset()
    {
        yield return new WaitForSeconds(1f); // C 스킬 지속 시간
        Wakamo_useC = false;
        Wakamo_mainTextC.color = Color.white;
        Debug.Log("Wakamo C 스킬 종료");
        targetUIImage.color = new Color(0.96f, 0.96f, 0.96f, 1f);
    }

    public void AL1S_SkillC()
    {
        if (Input.GetKeyDown(KeyCode.C) &&
            AL_1S_mainTextC.color == Color.white &&
            LockinManager.lastSelectedCharacter == "AL-1S" &&
            NEWUI._hp <= 50)
        {
            AL_1S_mainTextC.color = Color.gray;
            NEWUI._hp += 20;

            if (NEWUI.instance != null)
            {
                NEWUI.instance.UpdateHP();
            }
        }
    }

    public void AL1S_SkillQ()
    {
        if (Input.GetKeyDown(KeyCode.Q) &&
            AL_1S_mainTextQ.color == Color.white &&
            LockinManager.lastSelectedCharacter == "AL-1S")
        {
            AL_1S_mainTextQ.color = Color.gray;
            NEWSampleLaserClick.instance.AL1S_use_Q();
        }
    }

    public void AL1S_SkillX()
    {
        if (Input.GetKeyDown(KeyCode.X) &&
            AL_1S_mainTextX.color == Color.white &&
            LockinManager.lastSelectedCharacter == "AL-1S")
        {
            AL_1S_mainTextX.color = Color.gray;
            AL_1S_shootSkillX = true;
            AL_1S_laserClick.FireLaserX();
        }
    }

    private void AL_1S_CheckCooldownE()
    {
        if (!AL_1S_canUseSkillE)
        {
            currentCooldown -= Time.deltaTime;
            cooltime_AL_1S_eskill.text = Mathf.Ceil(currentCooldown).ToString() + "s";
            AL_1S_mainTextE.color = Color.gray;

            if (currentCooldown <= 0f)
            {
                AL_1S_canUseSkillE = true;
                cooltime_AL_1S_eskill.text = "";
                AL_1S_mainTextE.color = Color.white;
            }
        }
        else
        {
            AL_1S_mainTextE.color = Color.white;
        }
    }

    public static void AL1S_skillE()
    {
        AL_1S_canUseSkillE = false;
        currentCooldown = AL_1S_cooldownTime;
        Debug.Log("AL-1S E 키 사용! 20초 쿨타임 시작!");

        if (NEWSampleLaserClick.instance != null)
        {
            NEWSampleLaserClick.fireE();
        }
        else
        {
            Debug.LogWarning("NEWSampleLaserClick.instance is null when using Skill E");
        }
    }

    private IEnumerator ChangeUIImageColorFor15Seconds()
    {
        if (targetUIImage == null)
        {
            Debug.LogWarning("targetUIImage is not assigned!");
            yield break;
        }

        Wakamo_useE = true;

        Color originalColor = targetUIImage.color;
        Color f5f5f5Color = new Color(0.96f, 0.96f, 0.96f, 1f);
        Color angerColor = new Color(1f, 0.27f, 0f, 1f);

        float durationStart = 0.5f;
        float elapsed = 0f;
        while (elapsed < durationStart)
        {
            elapsed += Time.deltaTime;
            targetUIImage.color = Color.Lerp(f5f5f5Color, angerColor, elapsed / durationStart);
            yield return null;
        }

        yield return new WaitForSeconds(12.5f);

        float blinkDuration = 0.3f;
        float totalBlinkTime = 2f;
        elapsed = 0f;

        while (elapsed < totalBlinkTime)
        {
            elapsed += Time.deltaTime;
            if (((int)(elapsed / blinkDuration)) % 2 == 0)
                targetUIImage.color = angerColor;
            else
                targetUIImage.color = f5f5f5Color;
            yield return null;
        }

        targetUIImage.color = originalColor;
        Wakamo_useE = false;
    }
}
