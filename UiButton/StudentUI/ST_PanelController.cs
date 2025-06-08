using UnityEngine;

public class ST_PanelController : MonoBehaviour
{
    public GameObject al1SPanel; // AL-1S panel을 위한 변수
    public GameObject wakamoPanel;

    void Awake()
    {
        al1SPanel.SetActive(true);
        wakamoPanel.SetActive(false);
    }

    public void ShowAL1SPanel()
    {
        al1SPanel.SetActive(true); // AL-1S panel을 활성화
        wakamoPanel.SetActive(false);
    }

    public void ShowWakamoPanel()
    {
        wakamoPanel.SetActive(true);
        al1SPanel.SetActive(false);
    }
}