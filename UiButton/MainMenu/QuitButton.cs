using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void QuitApplication()
    {
        // 실제 게임에서는 Application.Quit()이 동작함
        Application.Quit();

        // Unity 에디터에서는 EditorApplication.isPlaying = false로 게임 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}