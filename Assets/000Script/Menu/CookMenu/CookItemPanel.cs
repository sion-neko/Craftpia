using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookItemPanel : MonoBehaviour
{
    string itemId = null;

    public void OnClickItemPanel()
    {

        if (itemId == null)
        {
            Debug.Log("大きい画面に表示するアイテムはありません。");
            return;
        }
        CookItemPreview.instance.ShowSelectedItem(itemId);
        CookExeButton.instance.itemId = itemId;
        Debug.Log("クリックしたアイテム番号：" + itemId);
    }

    public string getItemId()
    {
        return itemId;
    }

    public void setItemId(string newItemId)
    {
        itemId = newItemId;
    }
}
