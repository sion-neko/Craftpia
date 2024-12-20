public enum gameScene
{
    NormalScene,
    MenueScene
}

// 現在のゲームシーン(currentScene)を管理するクラス
public class DyanamicGameScene
{
    private gameScene _currentScene = gameScene.NormalScene;

    public void setCurrentScene(gameScene scene){
        this._currentScene = scene;
    }

    public gameScene getCurrentScene()
    {
        return _currentScene;
    }

}
