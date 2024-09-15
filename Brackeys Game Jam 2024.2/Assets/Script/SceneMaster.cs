using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMaster : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadCutScene()
    {
        SceneManager.LoadScene("CutScene");
    }

    public void LoadMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
