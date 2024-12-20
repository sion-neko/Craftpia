using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading;
using System.Threading.Tasks;

public class GameData : MonoBehaviour, ICookItemSozaiAcquisition
{
    public static GameData instance; // �C���X�^���X�̒�`

    Item[] itemDataArray;
    CookItem[] cookItemDataArray;
    ArrayList allItemDataArray;
    Dictionary<string, Item> id2Item;
    Dictionary<string, CookItem> id2CookItem;
    Dictionary<string, CookItem> cookItemName2item;
    Dictionary<string, Sprite> id2ItemImage;
    Dictionary<string, Item> id2AllItem;





    // Start is called before the first frame update
    async void Awake()
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


        //json����f�[�^�̓ǂݍ���
        //���V�s�@= [Item ��,Item ��]
        itemDataArray = new JsonReaderFromResourcesFolder().GetItemDataArray().gameItems;
        //���V�s�@= [CookItem �J���[,CookItem �����Ⴊ]
        cookItemDataArray = new JsonReaderFromResourcesFolder().GetRecipe().gameItems;

        allItemDataArray = new ArrayList(itemDataArray);
        allItemDataArray.AddRange(cookItemDataArray);



        //id��item�̎���

        //item�̎���
        id2Item = new Dictionary<string, Item>();
        //CookItem�̎���
        cookItemName2item = new Dictionary<string, CookItem>();
        id2CookItem = new Dictionary<string, CookItem>();

        id2AllItem = new Dictionary<string, Item>();

        foreach (Item item in itemDataArray)
        {
            id2Item.Add(item.id, item);
        }

        foreach (CookItem item in cookItemDataArray)
        {
            cookItemName2item.Add(item.name, item);
        }
        foreach (CookItem item in cookItemDataArray)
        {
            id2CookItem.Add(item.id, item);
        }

        foreach (Item item in allItemDataArray)
        {
            id2AllItem.Add(item.id, item);
        }

        id2ItemImage = new Dictionary<string, Sprite>();


        foreach (Item item in allItemDataArray)
        {
            Debug.Log(item.id);
            AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>(item.imgFileName);

            await handle.Task;
            id2ItemImage.Add(item.id, handle.Result);

        }
    }


    public Item getItem(string itemId)
    {
        return id2Item[itemId];
    }
    public Item getAllItem(string itemId)
    {
        return id2AllItem[itemId];
    }

    public string getId2AllItemName(string itemId)
    {
        return id2AllItem[itemId].name;
    }

    public CookItem getRecipeFromName(string cookItemName)
    {
        return cookItemName2item[cookItemName];
    }

    public CookItem getRecipe(string cookItemId)
    {
        return id2CookItem[cookItemId];
    }

    public Sozai[] getCookItemSozai(string cookItemId)
    {
        return id2CookItem[cookItemId].sozai;
    }

    public CookItem[] getCookItemDataArray()
    {
        return cookItemDataArray;
    }

    public Sprite getItemImage(string itemId)
    {
        return id2ItemImage[itemId];
    }

}
