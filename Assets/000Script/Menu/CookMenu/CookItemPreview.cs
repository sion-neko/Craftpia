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
        // シングルトンの呪文
        if (instance == null)
        {
            // 自身をインスタンスとする
            instance = this;
        }
        else
        {
            // インスタンスが複数存在しないように、既に存在していたら自身を消去する
            Destroy(gameObject);
        }
    }

    public void CleanSozaiPreview()
    {
        Text itemPreviewText = _itemPreview.GetComponentInChildren<Text>();
        itemPreviewText.text = "";
        GameObject itemPanelParent = _itemPreview.transform.Find("SozaiPanels").gameObject;
        

        //素材の画像を消す処理
        for (int i = 0; i < 6; i++)
        {
            GameObject itemPanel = itemPanelParent.transform.GetChild(i).gameObject;
            Image panelImage = itemPanel.transform.Find("Image").gameObject.GetComponent<Image>();
            panelImage.sprite = null;
            panelImage.color = new Color(0, 0, 0, 0);
            // アイテム個数を削除
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


            // パネルの取得
            GameObject itemPanel = itemPanelParent.transform.GetChild(idx).gameObject;


            // アイテム画像の表示
            Image panelImage = itemPanel.transform.Find("Image").gameObject.GetComponent<Image>();
            // アイテム画像の取得
            Sprite itemImage = GameData.instance.getItemImage(itemId);
            panelImage.sprite = Instantiate(itemImage);
            // アイテムの透明度を255にして表示する
            panelImage.color = new Color(255, 255, 255, 255);

            // アイテム個数の表示
            Text panelText = itemPanel.GetComponentInChildren<Text>();
            panelText.text = item_num.ToString();

            idx++;
        }
    }
}
