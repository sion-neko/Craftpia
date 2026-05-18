using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item :IAction
{
    //–Ø‚âÎ‚È‚Ç
    public string imgFileName;
    public string name;
    public string description;
    public string id;
    delegate void Use();

}

//============ˆÈ‰ºƒAƒCƒeƒ€==================
[System.Serializable]
public class EatItem : Item
{
    //ƒŠƒ“ƒS‚â–Ø‚ÌÀ
    //music SE;
    int heal_amount;

}

[System.Serializable]
public class Sozai
{
    public string id;
    public int num;
}

[System.Serializable]
public class CraftItem :Item
{
    // •€‚È‚Ç
    public Sozai[] sozai;
}

[System.Serializable]
public class CookItem : EatItem
{
    // —¿—
    public Sozai[] sozai;
}


