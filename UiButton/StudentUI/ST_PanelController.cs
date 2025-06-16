using UnityEngine;

public class ST_PanelController : MonoBehaviour
{
    public GameObject al1SPanel; // AL-1S panel을 위한 변수
    public GameObject wakamoPanel;
    public static string LastSelectedStudent = "AL1S";

    void Awake()
    {
        al1SPanel.SetActive(true);
        wakamoPanel.SetActive(false);
    }

    public void ShowAL1SPanel()
    {
        al1SPanel.SetActive(true); // AL-1S panel을 활성화
        wakamoPanel.SetActive(false);
        LastSelectedStudent = "AL1S";
    }

    public void ShowWakamoPanel()
    {
        wakamoPanel.SetActive(true);
        al1SPanel.SetActive(false);
        LastSelectedStudent = "Wakamo";
    }
}