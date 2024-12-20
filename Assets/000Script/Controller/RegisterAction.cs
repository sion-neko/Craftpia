using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TNRD;

public class RegisterAction : MonoBehaviour
{
    [SerializeField] InputKey _inputKey;
    public SerializableInterface<IPlayerAction> _player;
    public SerializableInterface<IManuPanelActivator> _manuPanelActivator;
    public SerializableInterface<IMenuUI> _menuUI;

    public DyanamicGameScene dyanamicGameScene;
    Dictionary<string, Dictionary<string, List<KeyCode>>> _state2Keyconfig = new Dictionary<string, Dictionary<string, List<KeyCode>>>();

    private void Start()
    {
        InitKeyconfig();
        dyanamicGameScene = new DyanamicGameScene();

        _inputKey.OnDownWASD.Subscribe(walkVector => { _player.Value.Walk(walkVector); });


        _inputKey.OnKeyDown.Subscribe(pressedKey =>
        {
            // 通常画面のキーコンフィグ
            if (dyanamicGameScene.getCurrentScene() == gameScene.NormalScene)
            {
                Debug.Log("NormalScene pressd " + pressedKey);
                // 接触ボタンのキーが入力された
                if (_state2Keyconfig["Normal"]["Contact"].Contains(pressedKey))
                {
                    _manuPanelActivator.Value.Contact();
                }

                if (_state2Keyconfig["Normal"]["OpenBag"].Contains(pressedKey))
                {
                    _manuPanelActivator.Value.OpenBag();
                }

                if (_state2Keyconfig["Normal"]["UseItem"].Contains(pressedKey))
                {
                    _player.Value.UseItem();
                }

            }

            // メニュー画面のキーコンフィグ
            if (dyanamicGameScene.getCurrentScene() == gameScene.MenueScene)
            {
                Debug.Log("MenuScene pressd " + pressedKey);
                // メニューを閉じるキーが入力された
                if (_state2Keyconfig["Menu"]["ClosePanel"].Contains(pressedKey))
                {
                    _menuUI.Value.ClosePanel();
                }

            }
        });


    }



    void InitKeyconfig()
    {
        //_stete2keyconfigに追加するための辞書
        Dictionary<string, List<KeyCode>> _tempAppend = new Dictionary<string, List<KeyCode>>();

        _tempAppend.Add("Contact", new List<KeyCode>() { KeyCode.F });
        _tempAppend.Add("OpenBag", new List<KeyCode>() { KeyCode.B });
        _tempAppend.Add("UseItem", new List<KeyCode>() { KeyCode.E });
        _state2Keyconfig.Add("Normal", new Dictionary<string, List<KeyCode>>(_tempAppend));


        _tempAppend.Clear();
        _tempAppend.Add("ClosePanel", new List<KeyCode>() { KeyCode.Escape });
        _state2Keyconfig.Add("Menu", new Dictionary<string, List<KeyCode>>(_tempAppend));

    }

    void NormalKey()
    {

    }


}
