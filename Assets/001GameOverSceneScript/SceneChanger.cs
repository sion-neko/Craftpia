using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    string MAIN_GAME_SCENE_NAME = "MainGameScenes";

    public void loadMainGameScene()
    {
        SceneManager.LoadScene(MAIN_GAME_SCENE_NAME);
    }
}
