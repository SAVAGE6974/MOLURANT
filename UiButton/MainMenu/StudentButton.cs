using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StudentButton : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("StudentUI");
    }
}
