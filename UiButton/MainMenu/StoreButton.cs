using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreButton : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("StoreUI");
    }
}
