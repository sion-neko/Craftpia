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
            Debug.Log("�傫����ʂɕ\������A�C�e���͂���܂���B");
            return;
        }
        BagItemPreview.instance.ShowSelectedItem(itemId);
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
