using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    string MAIN_GAME_SCENE_NAME = "MainGameScenes";
    string START_SCENE_NAME = "StartScenes";


    public void loadMainGameScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(MAIN_GAME_SCENE_NAME);
    }
    public void loadStartScene()
    {
        SceneManager.LoadScene(START_SCENE_NAME);
    }



}
