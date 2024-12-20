using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookTest : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameData gamedata;

    PlayerManager cook;
    private void Start()
    {
        cook = new PlayerManager(gamedata);
    }


    public void Cook()
    {
        //cook.remakeCanCookItemsList(player.getPlayerBag());
        cook.doCook("#100");
        Debug.Log("クックしました。");
    }

    //public void printRemakeList()
    //{
    //    cook.remakeCanCookItemsList(player.getPlayerBag());
    //    Debug.Log("作れる料理のリスト----------");
    //    foreach (string a in cook._canCookItems)
    //    {
    //        Debug.Log(a);
    //    }
    //}



}
