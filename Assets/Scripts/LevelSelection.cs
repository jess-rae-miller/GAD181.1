using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public int level;

    public void OpenScene()
    {
        FindAnyObjectByType<SaveManager>().ResetSceneDeaths();
        SceneManager.LoadScene("Level_" + level.ToString());
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void OpenLevelSelect()
    {
        SceneManager.LoadScene("Level_Select");
    }
}
