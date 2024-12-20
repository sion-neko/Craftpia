using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FieldObjectPickupManager : MonoBehaviour
{
    public static FieldObjectPickupManager instance; // インスタンスの定義

    public List<GameObject> pickupItemList { get; private set; } = new List<GameObject>();
    //[SerializeField] Player player;
    public bool isItemListUpdate = true;
    public Button contactButton;
    [SerializeField] GameObject selectPickupItemUI;
    [SerializeField] Transform selectPickupItemUIList;

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


    private void PickUpNearItemFirst(Player player)
    {
        if (pickupItemList.Count <= 1) return;
        Vector3 playerPos = player.transform.position;
        // 初期最小値を設定
        float minDirection = Vector3.Distance(pickupItemList[0].transform.position, playerPos);
        // 二つ目のアイテムから取得ポイントとの距離を計算
        for (int itemNum = 1; itemNum < pickupItemList.Count; itemNum++)
        {
            float direction = Vector3.Distance(pickupItemList[itemNum].transform.position, playerPos);
            // より近いオブジェクトを0番目の要素に代入
            if (minDirection > direction)
            {
                minDirection = direction;
                var temp = pickupItemList[0];
                pickupItemList[0] = pickupItemList[itemNum];
                pickupItemList[itemNum] = temp;
            }
        }
    }

    private void SetupContactButton(FieldObject attachItem, Player player)
    {
        contactButton.onClick.RemoveAllListeners();
        contactButton.gameObject.SetActive(true);
        contactButton.image.sprite = attachItem.fieldObjectImage;
        contactButton.onClick.AddListener(() => attachItem.pickUpItem(player));
    }

    private void clearSelectPickupItemUIList()
    {
        // すべての子オブジェクトを取得
        foreach (Transform n in selectPickupItemUIList)
        {
            GameObject.Destroy(n.gameObject);
        }
    }

    private void ShowSelectPickupItemUI(Player player)
    {
        clearSelectPickupItemUIList();

        int pickupItemIdx = 0;
        foreach (GameObject item in pickupItemList)
        {
            // 画面に入りきらないため、表示するUIを4つまでにする。
            if (pickupItemIdx < 4)
            {
                string selectItemId = item.GetComponent<FieldObject>().itemId;
                string selectItemName = GameData.instance.getId2AllItemName(selectItemId);
                float x_pos = 1370f;
                float y_pos = 590f - pickupItemIdx * 120;
                GameObject selectItemUI = Instantiate(selectPickupItemUI, new Vector3(x_pos, y_pos, 0), Quaternion.identity, selectPickupItemUIList);
                selectItemUI.GetComponentInChildren<Text>().text = selectItemName;

                // クリックしたらアイテムを取得するイベントをUIに登録する。
                EventTrigger trigger = selectItemUI.GetComponent<EventTrigger>();
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerClick;
                entry.callback.AddListener(_ => item.GetComponent<FieldObject>().pickUpItem(player));
                trigger.triggers.Add(entry);

                pickupItemIdx++;
            }

        }
    }

    public void UpdateContactButton(GameObject contactItem, Player player, bool isIncrease)
    {

        if (isIncrease)
        {
            pickupItemList.Add(contactItem);
        }
        else
        {
            pickupItemList.Remove(contactItem);
        }

        if (pickupItemList.Count > 0)
        {
            PickUpNearItemFirst(player);
            GameObject _nearItem = pickupItemList[0];
            SetupContactButton(_nearItem.GetComponent<FieldObject>(), player);
            ShowSelectPickupItemUI(player);
        }
        else
        {
            contactButton.gameObject.SetActive(false);
            contactButton.image.sprite = null;
            contactButton.onClick.RemoveAllListeners();
            clearSelectPickupItemUIList();
        }



    }







}
