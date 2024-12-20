using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TNRD;

public class BagContensRenderer : MonoBehaviour
{
    public SerializableInterface<IPlayerBagController> playerBag;
    GameObject currentBagPanel;


    public void OpenBagPanel(GameObject bagPanel)
    {
        // パネルに表示しているものを空にする
        InitItemPanel(bagPanel);
        // パネルに現在取得しているアイテムを表示する
        DisplayItems(bagPanel);

        passReOpenBagPanelFanction(bagPanel);

        currentBagPanel = bagPanel;
    }

    // 開いたbagpanelを再度開く
    // useItemを切り替えたとき使用する
    public void ReOpenBagPanel()
    {
        // パネルに表示しているものを空にする
        InitItemPanel(currentBagPanel);
        // パネルに現在取得しているアイテムを表示する
        DisplayItems(currentBagPanel);
    }
    public void InitItemPanel(GameObject bagMenuePanel)
    {
        GameObject itemPanelParent = bagMenuePanel.transform.Find("ItemPanel").gameObject;
        for (int i = 0; i < 9; i++)
        {
            GameObject itemPanel = itemPanelParent.transform.GetChild(i).gameObject;
            Image panelImage = itemPanel.transform.Find("Image").gameObject.GetComponent<Image>();
            panelImage.sprite = null;
            panelImage.color = new Color(0, 0, 0, 0);
        }

    }
    public void DisplayItems(GameObject bagMenuePanel)
    {
        Dictionary<string, int> bagSummary = playerBag.Value.getBagSummary();
        GameObject itemPanelParent = bagMenuePanel.transform.Find("ItemPanel").gameObject;

        
        //使用するアイテムを先頭に表示
        string useItemId = UseItem.instance.getUseItem();
        GameObject itemPanel;

        Debug.Log("useItem" + useItemId);
        //バックに入っていないuseItemIdを指定すると何も表示されなくなる(バグ)
        if (useItemId != null)
        {
            // パネルの取得
            itemPanel = itemPanelParent.transform.GetChild(0).gameObject;
            DisplayOneItem(useItemId, bagSummary[useItemId], itemPanel);
        }


        int idx = 1;

        foreach (var item in bagSummary)
        {
            string itemId = item.Key;
            int itemNum = item.Value;

            if(useItemId == item.Key)
            {
                continue;
            }

            // パネルの取得
            itemPanel = itemPanelParent.transform.GetChild(idx).gameObject;

            DisplayOneItem(itemId, itemNum, itemPanel);

           idx++;
        }
    }

    private void DisplayOneItem(string itemId, int itemNum, GameObject itemPanel)
    {     
        // パネルのスクリプトにitemIdを書き込む
        itemPanel.GetComponent<BagItemPanel>().setItemId(itemId);

        // アイテム画像の表示
        Image panelImage = itemPanel.transform.Find("Image").gameObject.GetComponent<Image>();
        // アイテム画像の取得
        Sprite itemImage = GameData.instance.getItemImage(itemId);
        panelImage.sprite = Instantiate(itemImage);
        // アイテムの透明度を255にして表示する
        panelImage.color = new Color(255, 255, 255, 255);

        // アイテム個数の表示
        Text panelText = itemPanel.GetComponentInChildren<Text>();
        panelText.text = itemNum.ToString();
    }

    private void passReOpenBagPanelFanction(GameObject bagPanel)
    {
        getChangeUseItemButton(bagPanel).onClick.AddListener(ReOpenBagPanel);
    }

    private Button getChangeUseItemButton(GameObject bagPanel)
    {
        return bagPanel.transform.Find("ChangeUseItemButton").gameObject.GetComponent<Button>();
    }

}
