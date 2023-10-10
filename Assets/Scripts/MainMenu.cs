using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string levelName;

    public void StartTheGame()
    {
        SceneManager.LoadScene(levelName);
    }
    public void ExitTheGame()
    {
        Application.Quit();
    }
}
