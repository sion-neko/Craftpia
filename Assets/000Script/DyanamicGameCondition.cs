public enum gameScene
{
    NormalScene,
    MenueScene
}

// ���݂̃Q�[���V�[��(currentScene)���Ǘ�����N���X
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
