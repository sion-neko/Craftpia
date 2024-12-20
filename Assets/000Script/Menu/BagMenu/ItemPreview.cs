using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPreview : MonoBehaviour
{

    public void ShowSelectedItem(string itemId, GameObject gameObject)
    {

        // �A�C�e���摜�̕\��
        Image panelImage = gameObject.transform.Find("Image").gameObject.GetComponent<Image>();
        // �A�C�e���摜�̎擾
        Sprite itemImage = GameData.instance.getItemImage(itemId);
        panelImage.sprite = Instantiate(itemImage);
        // �A�C�e���̓����x��255�ɂ��ĕ\������
        panelImage.color = new Color(255, 255, 255, 255);

        // �A�C�e�����̕\��
        string itemInfo;
        Text panelText = gameObject.GetComponentInChildren<Text>();
        itemInfo = "�u" +GameData.instance.getAllItem(itemId).name + "�v\n" + GameData.instance.getAllItem(itemId).description;
        panelText.text = itemInfo;

    }

}
