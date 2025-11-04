using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MeshCollider))]
public class FieldObject : MonoBehaviour
{

    public string itemId;
    public Sprite fieldObjectImage;

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
        }
        else
        {
            player.inItem(itemId);
            FieldObjectPickupManager.instance.UpdateContactButton(gameObject, player, false);
            Destroy(gameObject);    
        }
    }
}
