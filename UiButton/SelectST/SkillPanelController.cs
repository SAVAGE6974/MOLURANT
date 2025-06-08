using UnityEngine;

public class SkillPanelController : MonoBehaviour
{
    public GameObject introduceST;
    public GameObject eSkillPanel;
    public GameObject qSkillPanel;
    public GameObject cSkillPanel;
    public GameObject xSkillPanel;

    void Start()
    {
        // 초기화: 전부 안 보이게
        HideAllPanels();
    }

    public void showITSTPanel()
    {
        AfterHideAllPanels();
        introduceST.SetActive(true);
    }
    
    public void ShowESkillPanel()
    {
        AfterHideAllPanels();
        eSkillPanel.SetActive(true);
    }

    public void ShowQSkillPanel()
    {
        AfterHideAllPanels();
        qSkillPanel.SetActive(true);
    }

    public void ShowCSkillPanel()
    {
        AfterHideAllPanels();
        cSkillPanel.SetActive(true);
    }

    public void ShowXSkillPanel()
    {
        AfterHideAllPanels();
        xSkillPanel.SetActive(true);
    }

    void HideAllPanels()
    {
        introduceST.SetActive(true);
        eSkillPanel.SetActive(false);
        qSkillPanel.SetActive(false);
        cSkillPanel.SetActive(false);
        xSkillPanel.SetActive(false);
    }

    void AfterHideAllPanels()
    {
        introduceST.SetActive(false);
        eSkillPanel.SetActive(false);
        qSkillPanel.SetActive(false);
        cSkillPanel.SetActive(false);
        xSkillPanel.SetActive(false);
    }
}