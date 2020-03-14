using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMeun : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void QuitGAme()
    {
        //Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
