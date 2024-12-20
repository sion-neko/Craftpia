using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanelManager : MonoBehaviour, IMenuUI
{

    [SerializeField] RegisterAction _registerAction;

    private GameObject _manuPanelInstance;


    public GameObject InstiateManuPanel(GameObject panel)
    {
        if (_manuPanelInstance == null)
        {
            //Debug.Log("open panel");
            _manuPanelInstance = Instantiate(panel);

            // TODO:�Ȍ��ɏ���
            // �N���t�g�p�l���̕���{�^����ClosePanel()�̏�����������B
            // (�N���t�g�p�l���ɏ���������������registerAction���p�l��������Q�Ƃ��邱�Ƃ��ł��Ȃ�)
            getCloseButton().onClick.AddListener(CloseButtonClicked);

            _registerAction.dyanamicGameScene.setCurrentScene(gameScene.MenueScene);
            Time.timeScale = 0f;
        }
        return _manuPanelInstance;
    }
    public void ClosePanel()
    {
        // �匳��PanelUI���폜���ăp�l���\��������   
        Time.timeScale = 1f;
        Destroy(_manuPanelInstance);

        // �Q�[���V�[����MenuScene����NormalScene�ɕύX����
        _registerAction.dyanamicGameScene.setCurrentScene(gameScene.NormalScene);
    }

    // �p�l���̃N���[�Y�{�^�����������Ƃ��̏���
    private void CloseButtonClicked()
    {
        ClosePanel();
    }

    private Button getCloseButton()
    {

        return _manuPanelInstance.transform.Find("CloseButton").gameObject.GetComponent<Button>();

    }

    public GameObject getManuPanelInstance()
    {
        return _manuPanelInstance;
    }
}
