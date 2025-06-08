using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoEndSceneLoader : MonoBehaviour
{
    public VideoPlayer videoPlayer;      // 비디오 플레이어 컴포넌트
    public string MainMenuScene;         // 이동할 씬 이름

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;  // 영상이 끝나면 호출
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(MainMenuScene);  
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // 원하는 씬으로 이동
    }
}