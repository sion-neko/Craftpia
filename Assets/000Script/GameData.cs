using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading;
using System.Threading.Tasks;

public class GameData : MonoBehaviour, ICookItemSozaiAcquisition
{
    public static GameData instance; // インスタンスの定義

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


        //jsonからデータの読み込み
        //レシピ　= [Item 木,Item 石]
        itemDataArray = new JsonReaderFromResourcesFolder().GetItemDataArray().gameItems;
        //レシピ　= [CookItem カレー,CookItem 肉じゃが]
        cookItemDataArray = new JsonReaderFromResourcesFolder().GetRecipe().gameItems;

        allItemDataArray = new ArrayList(itemDataArray);
        allItemDataArray.AddRange(cookItemDataArray);



        //idとitemの辞書

        //itemの辞書
        id2Item = new Dictionary<string, Item>();
        //CookItemの辞書
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
