using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagItemPanel : MonoBehaviour
{
    string itemId = null;

    //[SerializeField] ItemPreview _sip;
    public void OnClickItemPanel()
    {

        if (itemId == null)
        {
            Debug.Log("大きい画面に表示するアイテムはありません。");
            return;
        }
        BagItemPreview.instance.ShowSelectedItem(itemId);
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
