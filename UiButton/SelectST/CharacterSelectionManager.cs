using UnityEngine;
using UnityEngine.SceneManagement;

public class LockinManager : MonoBehaviour
{
    public static string lastSelectedCharacter; // static으로 변경

    public void SelectCharacter(string characterName)
    {
        lastSelectedCharacter = characterName;
    }

    public void ConfirmSelection()
    {
        if (!string.IsNullOrEmpty(lastSelectedCharacter))
        {
            Debug.Log(lastSelectedCharacter + " 캐릭터 선택완료");
            SceneManager.LoadScene("NEWGameScene");
        }
        else
        {
            Debug.Log("아직 어떤 캐릭터도 선택되지 않았습니다.");
        }
    }
}