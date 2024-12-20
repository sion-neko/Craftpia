using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookExeButton : MonoBehaviour
{
    public static CookExeButton instance;
    public string itemId;

    private Player _player;
    private GameObject _cookPanel;
    private CookItem[] _recipeArray;

    void Awake()
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

    public void exeCook()
    {
        _player.Cook(itemId);
        PasteRecipe();
    }

    // 作成ボタン押したときにレシピを再表示する
    public void PasteRecipe()
    {
        // パネルに表示しているものを空にする
        InitItemPanel();
        CookItemPreview.instance.CleanSozaiPreview();

        List<CookItem> cookableItemList = GenerateCookableItemList(_player.getBagSummary());
        // パネルに現在作成できる料理を表示する
        DisplayItems(cookableItemList);
    }

    // クラフトボックスからクックメニューを開いたときにレシピを表示する
    public void PasteRecipe(GameObject cookPanel, Player player, CookItem[] recipeArray)
    {
        this._cookPanel = cookPanel;
        this._player = player;
        this._recipeArray = recipeArray;

        // パネルに表示しているものを空にする
        InitItemPanel();
        List<CookItem> cookableItemList = GenerateCookableItemList(player.getBagSummary());
        // パネルに現在作成できる料理を表示する
        DisplayItems(cookableItemList);
    }
    private void InitItemPanel()
    {
        GameObject itemPanelParent = _cookPanel.transform.Find("ItemPanel").gameObject;
        for (int i = 0; i < 10; i++)
        {
            GameObject itemPanel = itemPanelParent.transform.GetChild(i).gameObject;
            Image panelImage = itemPanel.transform.Find("Image").gameObject.GetComponent<Image>();
            panelImage.sprite = null;
            panelImage.color = new Color(0, 0, 0, 0);
        }


    }
    private void DisplayItems(List<CookItem> recipeArray)
    {

        GameObject itemPanelParent = _cookPanel.transform.Find("ItemPanel").gameObject;
        int idx = 0;

        foreach (CookItem recipe in recipeArray)
        {
            string itemId = recipe.id;

            // パネルの取得
            GameObject itemPanel = itemPanelParent.transform.GetChild(idx).gameObject;

            // パネルのスクリプトにitemIdを書き込む
            itemPanel.GetComponent<CookItemPanel>().setItemId(itemId);

            // アイテム画像の表示
            Image panelImage = itemPanel.transform.Find("Image").gameObject.GetComponent<Image>();
            // アイテム画像の取得
            Sprite itemImage = GameData.instance.getItemImage(itemId);
            panelImage.sprite = Instantiate(itemImage);
            // アイテムの透明度を255にして表示する
            panelImage.color = new Color(255, 255, 255, 255);


            idx++;
        }

    }

    // 作成可能な料理のリストを作成する
    List<CookItem> GenerateCookableItemList(Dictionary<string, int> playerBagSummary)
    {
        List<CookItem> cookableItemList = new List<CookItem>();
        foreach (CookItem recipe in _recipeArray)
        {
            bool cookable = true;
            // 各素材の個数がバッグの中身より多いか判定する
            foreach (Sozai need_sozai in recipe.sozai)
            {

                if (!biggerQuantity(playerBagSummary, need_sozai.id, need_sozai.num))
                {
                    cookable = false;
                }
            }

            // 料理可能なものをリストに追加する
            if (cookable)
            {
                cookableItemList.Add(recipe);
            }

        }

        return cookableItemList;
    }

    bool biggerQuantity(Dictionary<string, int> playerBagSummary, string id, int quantity)
    {
        bool retval = false;
        if (playerBagSummary.ContainsKey(id))
        {
            if (playerBagSummary[id] >= quantity)
            {
                retval = true;
            }
        }

        return retval;

    }
}