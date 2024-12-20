using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseItem : MonoBehaviour
{
    public static UseItem instance;
    string _useItemId = null;
    [SerializeField] GameObject useItemBackgroundPanel;

    // Start is called before the first frame update
    void Start()
    {
        // �V���O���g���̎���
        if (instance == null)
        {
            // ���g���C���X�^���X�Ƃ���
            instance = this;
        }
        else
        {
            // �C���X�^���X���������݂��Ȃ��悤�ɁA���ɑ��݂��Ă����玩�g����������
            Destroy(gameObject);
        }
    }


    public string getUseItem()
    {
        return _useItemId;
    }


    public void updateUseItem(string itemId)
    {
        _useItemId = itemId;
        Sprite useItemImage = GameData.instance.getItemImage(itemId);
        useItemBackgroundPanel.gameObject.GetComponent<Image>().sprite = useItemImage;
        useItemBackgroundPanel.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        Debug.Log(_useItemId + "���g�p�A�C�e���ɃZ�b�g���܂���");
    }

}
