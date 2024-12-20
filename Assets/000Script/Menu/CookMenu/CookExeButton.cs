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

    public void exeCook()
    {
        _player.Cook(itemId);
        PasteRecipe();
    }

    // �쐬�{�^���������Ƃ��Ƀ��V�s���ĕ\������
    public void PasteRecipe()
    {
        // �p�l���ɕ\�����Ă�����̂���ɂ���
        InitItemPanel();
        CookItemPreview.instance.CleanSozaiPreview();

        List<CookItem> cookableItemList = GenerateCookableItemList(_player.getBagSummary());
        // �p�l���Ɍ��ݍ쐬�ł��闿����\������
        DisplayItems(cookableItemList);
    }

    // �N���t�g�{�b�N�X����N�b�N���j���[���J�����Ƃ��Ƀ��V�s��\������
    public void PasteRecipe(GameObject cookPanel, Player player, CookItem[] recipeArray)
    {
        this._cookPanel = cookPanel;
        this._player = player;
        this._recipeArray = recipeArray;

        // �p�l���ɕ\�����Ă�����̂���ɂ���
        InitItemPanel();
        List<CookItem> cookableItemList = GenerateCookableItemList(player.getBagSummary());
        // �p�l���Ɍ��ݍ쐬�ł��闿����\������
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

            // �p�l���̎擾
            GameObject itemPanel = itemPanelParent.transform.GetChild(idx).gameObject;

            // �p�l���̃X�N���v�g��itemId����������
            itemPanel.GetComponent<CookItemPanel>().setItemId(itemId);

            // �A�C�e���摜�̕\��
            Image panelImage = itemPanel.transform.Find("Image").gameObject.GetComponent<Image>();
            // �A�C�e���摜�̎擾
            Sprite itemImage = GameData.instance.getItemImage(itemId);
            panelImage.sprite = Instantiate(itemImage);
            // �A�C�e���̓����x��255�ɂ��ĕ\������
            panelImage.color = new Color(255, 255, 255, 255);


            idx++;
        }

    }

    // �쐬�\�ȗ����̃��X�g���쐬����
    List<CookItem> GenerateCookableItemList(Dictionary<string, int> playerBagSummary)
    {
        List<CookItem> cookableItemList = new List<CookItem>();
        foreach (CookItem recipe in _recipeArray)
        {
            bool cookable = true;
            // �e�f�ނ̌����o�b�O�̒��g��葽�������肷��
            foreach (Sozai need_sozai in recipe.sozai)
            {

                if (!biggerQuantity(playerBagSummary, need_sozai.id, need_sozai.num))
                {
                    cookable = false;
                }
            }

            // �����\�Ȃ��̂����X�g�ɒǉ�����
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