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
            Debug.Log("�傫����ʂɕ\������A�C�e���͂���܂���B");
            return;
        }
        CookItemPreview.instance.ShowSelectedItem(itemId);
        CookExeButton.instance.itemId = itemId;
        Debug.Log("�N���b�N�����A�C�e���ԍ��F" + itemId);
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
