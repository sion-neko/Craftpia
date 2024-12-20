using System.Collections.Generic;

interface IManager
{
    void doCook(string cookItem_id);
    void pickUpItem(string id, int quantity);
    Dictionary<string, int> getBagSummary();
    Dictionary<Item, int> getUseItem();
}
