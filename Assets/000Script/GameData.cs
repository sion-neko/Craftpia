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

    CookItem[] _cookItemDataArray;
    ArrayList _allItemDataArray;
    Dictionary<string, Item> _id2Item;
    Dictionary<string, CookItem> _id2CookItem;
    Dictionary<string, CookItem> _cookItemName2item;
    Dictionary<string, Sprite> _id2ItemImage;
    Dictionary<string, Item> _id2AllItem;
    Dictionary<int, PlayerLevelData> _level2Data;

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
        GameDataJsonReceiver gameDataJsonReceiver = new JsonReaderFromResourcesFolder().getGameDataFromJson();

        //itemDataArray = [Item 木,Item 石]
        Item[] itemDataArray = gameDataJsonReceiver.gameItems;
        //cookItemDataArray = [CookItem カレー,CookItem 肉じゃが]
        _cookItemDataArray = gameDataJsonReceiver.gameCookItems;
        //playerLevelDataArray = [playerLevelData 1, ]
        PlayerLevelData[] playerLevelDataArray = gameDataJsonReceiver.playerLevelData;

        Debug.Log("level:  " + playerLevelDataArray[0].level);
        Debug.Log("level1sozai: " + playerLevelDataArray[0].nextLevelRequaimets[0].id);
        Debug.Log("level1HP: " + playerLevelDataArray[0].status.hp);


        // 全itemのリストを作成
        _allItemDataArray = new ArrayList(itemDataArray);
        _allItemDataArray.AddRange(_cookItemDataArray);


        //idとitemの辞書

        //itemの辞書
        _id2Item = new Dictionary<string, Item>();
        //CookItemの辞書
        _cookItemName2item = new Dictionary<string, CookItem>();
        _id2CookItem = new Dictionary<string, CookItem>();

        _id2AllItem = new Dictionary<string, Item>();

        _level2Data = new Dictionary<int, PlayerLevelData>();

        foreach (Item item in itemDataArray)
        {
            _id2Item.Add(item.id, item);
        }

        foreach (CookItem item in _cookItemDataArray)
        {
            _cookItemName2item.Add(item.name, item);
        }
        foreach (CookItem item in _cookItemDataArray)
        {
            _id2CookItem.Add(item.id, item);
        }

        foreach (Item item in _allItemDataArray)
        {
            _id2AllItem.Add(item.id, item);
        }

        _id2ItemImage = new Dictionary<string, Sprite>();
        foreach (Item item in _allItemDataArray)
        {
            Debug.Log(item.id);
            AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>(item.imgFileName);

            await handle.Task;
            _id2ItemImage.Add(item.id, handle.Result);

        }

        foreach (PlayerLevelData data in playerLevelDataArray)
        {
            _level2Data.Add(data.level, data);
        }
    }


    public Item getItem(string itemId)
    {
        return _id2Item[itemId];
    }
    public Item getAllItem(string itemId)
    {
        return _id2AllItem[itemId];
    }

    public string getId2AllItemName(string itemId)
    {
        return _id2AllItem[itemId].name;
    }

    public CookItem getRecipeFromName(string cookItemName)
    {
        return _cookItemName2item[cookItemName];
    }

    public CookItem getRecipe(string cookItemId)
    {
        return _id2CookItem[cookItemId];
    }

    public Sozai[] getCookItemSozai(string cookItemId)
    {
        return _id2CookItem[cookItemId].sozai;
    }

    public CookItem[] getCookItemDataArray()
    {
        return _cookItemDataArray;
    }

    public Sprite getItemImage(string itemId)
    {
        return _id2ItemImage[itemId];
    }

    public PlayerLevelData getPlayerLevelData(int level)
    {
        return _level2Data[level];
    }


}

public class GameDataJsonReceiver
{
    public Item[] gameItems;
    public CookItem[] gameCookItems;
    public PlayerLevelData[] playerLevelData;

    // public EatItem[] gameEatItems;
    // public CraftItem[] gameCraftItems;
}
public class JsonReaderFromResourcesFolder
{
    public GameDataJsonReceiver getGameDataFromJson()
    {
        string filePath = "json/game_data";
        TextAsset file = Resources.Load(filePath) as TextAsset;
        GameDataJsonReceiver gameDataJsonReceiver = JsonUtility.FromJson<GameDataJsonReceiver>(file.text);
        return gameDataJsonReceiver;
    }
}