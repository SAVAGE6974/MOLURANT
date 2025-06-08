using UnityEngine;

public class PanelController_AL1S : MonoBehaviour
{
    public GameObject al1SPanel; // AL-1S panel을 위한 변수

    void Awake()
    {
        al1SPanel.SetActive(false);
    }

    public void ShowAL1SPanel()
    {
        al1SPanel.SetActive(true); // AL-1S panel을 활성화
    }

    public void HideAL1SPanel()
    {
        al1SPanel.SetActive(false); // AL-1S panel을 비활성화
    }
}