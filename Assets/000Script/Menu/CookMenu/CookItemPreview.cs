using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookItemPreview : MonoBehaviour
{
    public static CookItemPreview instance;
    [SerializeField] ItemPreview _itemPreview;
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

    public void CleanSozaiPreview()
    {
        Text itemPreviewText = _itemPreview.GetComponentInChildren<Text>();
        itemPreviewText.text = "";
        GameObject itemPanelParent = _itemPreview.transform.Find("SozaiPanels").gameObject;
        

        //�f�ނ̉摜����������
        for (int i = 0; i < 6; i++)
        {
            GameObject itemPanel = itemPanelParent.transform.GetChild(i).gameObject;
            Image panelImage = itemPanel.transform.Find("Image").gameObject.GetComponent<Image>();
            panelImage.sprite = null;
            panelImage.color = new Color(0, 0, 0, 0);
            // �A�C�e�������폜
            Text panelText = itemPanel.GetComponentInChildren<Text>();
            panelText.text = "";
        }
    }

    public void ShowSelectedItem(string itemId)
    {
        CleanSozaiPreview();

        _itemPreview.ShowSelectedItem(itemId, gameObject);

        SozaiPreview(itemId);
    }

    public void SozaiPreview(string cookItemId)
    {

        GameObject itemPanelParent = _itemPreview.transform.Find("SozaiPanels").gameObject;
        Sozai[] _sozai = GameData.instance.getCookItemSozai(cookItemId);



        int idx = 0;
        foreach (var item in _sozai)
        {
            string itemId = item.id;
            int item_num = item.num;


            // �p�l���̎擾
            GameObject itemPanel = itemPanelParent.transform.GetChild(idx).gameObject;


            // �A�C�e���摜�̕\��
            Image panelImage = itemPanel.transform.Find("Image").gameObject.GetComponent<Image>();
            // �A�C�e���摜�̎擾
            Sprite itemImage = GameData.instance.getItemImage(itemId);
            panelImage.sprite = Instantiate(itemImage);
            // �A�C�e���̓����x��255�ɂ��ĕ\������
            panelImage.color = new Color(255, 255, 255, 255);

            // �A�C�e�����̕\��
            Text panelText = itemPanel.GetComponentInChildren<Text>();
            panelText.text = item_num.ToString();

            idx++;
        }
    }
}
