using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MeshCollider))]
public class FieldObject : MonoBehaviour
{

    public string itemId;
    public Sprite fieldObjectImage;
    [SerializeField] GameObject _levelUpMenuPanel;
    [SerializeField] MenuPanelManager _menuPanelManager;

    void OnTriggerEnter(Collider other)
    {

        //アイテム画像を取得
        if (!fieldObjectImage)
        {
            fieldObjectImage = GameData.instance.getItemImage(itemId);
        }

        //コンタクトボタンの更新
        if (other.tag == "Player")
        {
            FieldObjectPickupManager.instance.UpdateContactButton(gameObject, other.gameObject.GetComponent<Player>(), true);
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            FieldObjectPickupManager.instance.UpdateContactButton(gameObject, other.gameObject.GetComponent<Player>(), false);
        }
    }


    public void pickUpItem(Player player)
    {
        if (itemId == "#1000")
        {
            // TODO: LevelUP処理
            // LevelUP画面を表示させる
            Debug.Log("LevelUP処理");

            GameObject _panelInstance = _menuPanelManager.InstiateManuPanel(_levelUpMenuPanel);
            InitItemPanel(_panelInstance);
            DisplayItems(_panelInstance, player);


        }
        else
        {
            player.inItem(itemId);
            FieldObjectPickupManager.instance.UpdateContactButton(gameObject, player, false);
            Destroy(gameObject);
        }
    }

    public void InitItemPanel(GameObject levelUpMenuePanel)
    {
        GameObject itemPanelParent = levelUpMenuePanel.transform.Find("ItemPanel").gameObject;
        GameObject viewItemPanel = itemPanelParent.transform.Find("ViewItemPanel").gameObject;
        GameObject sozaiPanelParent = viewItemPanel.transform.Find("SozaiPanels").gameObject;
        for (int i = 0; i < 5; i++)
        {
            GameObject itemPanel = sozaiPanelParent.transform.GetChild(i).gameObject;
            Text panelText = itemPanel.GetComponentInChildren<Text>();
            panelText.text = "";

            Image panelImage = itemPanel.transform.Find("Image").gameObject.GetComponent<Image>();
            panelImage.sprite = null;
            panelImage.color = new Color(0, 0, 0, 0);
        }

        Button levelUpButton = itemPanelParent.transform.Find("レベルアップボタン").gameObject.GetComponent<Button>();
        levelUpButton.interactable = true;

    }

    public void DisplayItems(GameObject levelUpMenuePanel, Player player)
    {
        int currentLevel = player.getPlayerLevel();
        Sozai[] nextLevelRequaimets = GameData.instance.getPlayerLevelData(currentLevel).nextLevelRequaimets;
        GameObject itemPanelParent = levelUpMenuePanel.transform.Find("ItemPanel").gameObject;
        GameObject viewItemPanel = itemPanelParent.transform.Find("ViewItemPanel").gameObject;

        // 現在のレベルを表示
        Text levelText = viewItemPanel.transform.Find("Level").gameObject.GetComponent<Text>();
        levelText.text = currentLevel.ToString() + "→" + (currentLevel + 1).ToString();


        // 必要な素材を表示
        GameObject sozaiPanelParent = viewItemPanel.transform.Find("SozaiPanels").gameObject;
        GameObject itemPanel;

        int idx = 0;
        foreach (Sozai sozai in nextLevelRequaimets)
        {
            Debug.Log("書き込み中: " + sozai.id + ":" + sozai.num);
            string itemId = sozai.id;
            int itemNum = sozai.num;

            // パネルの取得
            itemPanel = sozaiPanelParent.transform.GetChild(idx).gameObject;

            DisplayOneItem(itemId, itemNum, itemPanel);

            idx++;
        }

        // LevelUpボタンにLevelUp関数を登録
        Button levelUpButton = itemPanelParent.transform.Find("レベルアップボタン").gameObject.GetComponent<Button>();
        PlayerLevelData playerLevelData = GameData.instance.getPlayerLevelData(player.getPlayerLevel());
        foreach (Sozai sozai in playerLevelData.nextLevelRequaimets)
        {
            if (!player.existSozai(sozai))
            {
                Debug.Log("素材が足りません");
                levelUpButton.interactable = false;
                return;
            }

        }
        levelUpButton.onClick.AddListener(() => StartCoroutine(player.PlayerLevelUp()));
        levelUpButton.onClick.AddListener(_menuPanelManager.ClosePanel);


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
}
