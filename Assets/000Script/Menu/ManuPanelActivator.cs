using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManuPanelActivator: MonoBehaviour, IManuPanelActivator
{
    [SerializeField] Button _actionButton;

    [SerializeField] GameObject _bagPanel;
    [SerializeField] MenuPanelManager _menuPanelManager;
    [SerializeField] BagContensRenderer _bagContensRenderer;

    public void Contact()
    {
        _actionButton.onClick.Invoke();
    }
    public void OpenBag()
    {
        // �p�l���̍쐬
        GameObject _panelInstance = _menuPanelManager.InstiateManuPanel(_bagPanel);
        // �p�l���Ɍ��ݏ������Ă���A�C�e����\������
        _bagContensRenderer.OpenBagPanel(_panelInstance);
    }
}
