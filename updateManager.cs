using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class updateManager : MonoBehaviour
{
    [NotNull] public GameObject AL1ScancleButton;
    public Text attackText;
    public Text hpText;
    public Text cooltimeE;

    [NotNull] public GameObject WakamocancleButton;
    public Text Wakamo_attackText;
    public Text Wakamo_hpText;
    public Text Wakamo_cooltimeE;

    public Text WakmodiamondText;
    public Text Al1SdiamondText;

    public float sharedDiamond = 1000f;

    private void Awake()
    {
        AL1ScancleButton.SetActive(false);
        WakamocancleButton.SetActive(false);
        sharedDiamond = 1000f;


        // 다이아 불러오기
        sharedDiamond = PlayerPrefs.GetFloat("Shared_Diamond", 1000f);

        // 캐릭터 능력치 불러오기 및 텍스트 초기화
        Wakamo_attackText.text = NEWEnemy.Wakamo_damage.ToString("F1");
        Wakamo_hpText.text = MainUI._hp.ToString("F1");
        Wakamo_cooltimeE.text = NEWGamemenager.Wakamo_cooldownTime.ToString("F1");

        attackText.text = NEWEnemy.AL_1S_damage.ToString("F1");
        hpText.text = MainUI._hp.ToString("F1");
        cooltimeE.text = NEWGamemenager.AL_1S_cooldownTime.ToString("F1");

        UpdateDiamondText();
    }

    private void UpdateDiamondText()
    {
        WakmodiamondText.text = sharedDiamond.ToString();
        Al1SdiamondText.text = sharedDiamond.ToString();
    }

    public void WakamoopenUpdate() => WakamocancleButton.SetActive(true);
    public void WakamoCloseUpdate() => WakamocancleButton.SetActive(false);
    public void AL1SopenUpdate() => AL1ScancleButton.SetActive(true);
    public void AL1ScancelButton() => AL1ScancleButton.SetActive(false);

    public void WakamoattackText()
    {
        if (sharedDiamond >= 100)
        {
            NEWEnemy.Wakamo_damage += NEWEnemy.Wakamo_damage * 0.1f;
            sharedDiamond -= 100;

            PlayerPrefs.SetFloat("Wakamo_Damage", NEWEnemy.Wakamo_damage);
            PlayerPrefs.SetFloat("Shared_Diamond", sharedDiamond);
            PlayerPrefs.Save();

            Wakamo_attackText.text = NEWEnemy.Wakamo_damage.ToString("F1");
            UpdateDiamondText();
        }
    }

    public void WakamoHPText()
    {
        if (sharedDiamond >= 100)
        {
            MainUI._hp += 10f;
            sharedDiamond -= 100;

            PlayerPrefs.SetFloat("Wakamo_HP", MainUI._hp);
            PlayerPrefs.SetFloat("Shared_Diamond", sharedDiamond);
            PlayerPrefs.Save();

            Wakamo_hpText.text = MainUI._hp.ToString("F1");
            UpdateDiamondText();
        }
    }

    public void WakamocooltimeE()
    {
        if (sharedDiamond >= 100)
        {
            NEWGamemenager.Wakamo_cooldownTime -= NEWGamemenager.Wakamo_cooldownTime * 0.1f;
            sharedDiamond -= 100;

            PlayerPrefs.SetFloat("WakamoskillCoolE", NEWGamemenager.Wakamo_cooldownTime);
            PlayerPrefs.SetFloat("Shared_Diamond", sharedDiamond);
            PlayerPrefs.Save();

            Wakamo_cooltimeE.text = NEWGamemenager.Wakamo_cooldownTime.ToString("F1");
            UpdateDiamondText();
        }
    }

    public void AL1SattackText()
    {
        if (sharedDiamond >= 100)
        {
            NEWEnemy.AL_1S_damage += NEWEnemy.AL_1S_damage * 0.1f;
            sharedDiamond -= 100;

            PlayerPrefs.SetFloat("AL1S_Damage", NEWEnemy.AL_1S_damage);
            PlayerPrefs.SetFloat("Shared_Diamond", sharedDiamond);
            PlayerPrefs.Save();

            attackText.text = NEWEnemy.AL_1S_damage.ToString("F1");
            UpdateDiamondText();
        }
    }

    public void AL1SHPText()
    {
        if (sharedDiamond >= 100)
        {
            MainUI._hp += 10f;
            sharedDiamond -= 100;

            PlayerPrefs.SetFloat("HP", MainUI._hp);
            PlayerPrefs.SetFloat("Shared_Diamond", sharedDiamond);
            PlayerPrefs.Save();

            hpText.text = MainUI._hp.ToString("F1");
            UpdateDiamondText();
        }
    }

    public void AL1ScooltimeE()
    {
        if (sharedDiamond >= 100)
        {
            NEWGamemenager.AL_1S_cooldownTime -= NEWGamemenager.AL_1S_cooldownTime * 0.1f;
            sharedDiamond -= 100;

            PlayerPrefs.SetFloat("AL1SskillCoolE", NEWGamemenager.AL_1S_cooldownTime);
            PlayerPrefs.SetFloat("Shared_Diamond", sharedDiamond);
            PlayerPrefs.Save();

            cooltimeE.text = NEWGamemenager.AL_1S_cooldownTime.ToString("F1");
            UpdateDiamondText();
        }
    }
}
