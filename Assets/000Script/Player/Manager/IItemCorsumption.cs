using System.Collections.Generic;
public interface IItemConsumption
{
    void inItem(string itemId, int num);
    void subItemQuantity(string item_id, int num);

    //バッグに入れることができるアイテムのidのリスト
    List<string> createInItemArgumentList();

    //バッグの中身
    Dictionary<string, int> getBagSummary();

    //バッグがいっぱいかどうか
    bool isMaxBag();
}