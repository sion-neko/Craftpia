using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour, IPlayerAction, IPlayerBagController
{
    IOno _ono;
    [SerializeField] GameData gamedata;
    IManager _manager;
    [SerializeField] IPlayerMove playerMove;
    PlayerHP playerHP;
    int playerLevel;
    [SerializeField] Slider staminaSlider;
    int walkSpeed;


    
    string GAME_OVER_SCENE_NAME = "GameOverScenes";

    // ひとつ前のwalkVectorを保存する。
    private bool beforeIsZero = false;

    private void Start()
    {
        _ono = new Ono(1, 1);
        _manager = new PlayerManager(gamedata);
        playerMove = GetComponent<Walk>();
        playerLevel = 1;
        int maxStamina = 1000;
            // TODO: 初期化できるようにする(GameDataのAwakeの処理が終わっておらず読み込めない)
            // GameData.instance.getPlayerLevelData(playerLevel).status.hp;
        playerHP = new PlayerHP(maxStamina);
        staminaSlider.maxValue = maxStamina;
        walkSpeed = 1;

        
    }
    public void inItem(string id, int quantity = 1)
    {
        _manager.pickUpItem(id, quantity);
    }

    public void Cook(string cookItem_id)
    {
        _manager.doCook(cookItem_id);
    }

    public void PlayerLevelUp()
    {
        PlayerLevelData playerLevelData = GameData.instance.getPlayerLevelData(playerLevel);
        foreach(Sozai sozai in playerLevelData.nextLevelRequaimets)
        {
            if(!_manager.exitSozai(sozai))
            {
                Debug.Log("素材が足りません");
                return;
            }
        }
        foreach(Sozai sozai in playerLevelData.nextLevelRequaimets)
        {
            _manager.consumeSozai(sozai);
        }
        playerLevel++;
        playerHP.setHP(playerLevelData.status.hp);
        staminaSlider.maxValue = playerHP.getHP();
        staminaSlider.value = playerHP.getHP();
        walkSpeed = playerLevelData.status.speed;
    }

    public void Walk(Vector2 walkVector)
    {
        if (walkVector.magnitude > 0)
        {
            playerMove.walk(walkVector*walkSpeed);
            beforeIsZero = false;


            // 歩くたびにHPを減らす。
            if (playerHP.ConsumeHP(Config.CONSUME_HP_SPEED))
            {
                staminaSlider.value = playerHP.getHP();
            }
            else
            {
                Debug.Log("体力０");
                SceneManager.LoadScene(GAME_OVER_SCENE_NAME);
            }

        }
        else
        {
            // 連続でvectorが0の場合スルーさせたい
            if (!beforeIsZero)
            {
                // プレイヤーを止めないといけないので
                // 1回は処理を実行する。
                playerMove.walk(walkVector);
                beforeIsZero = true;
            }

        }

    }

    public void UseItem()
    {

    }

    
    public int getPlayerLevel() { return playerLevel; }

    public int getPlayerOnoLv() { return _ono.getLv(); }

    public int getPlayerOnoAtk() { return _ono.getAtk(); }

    public Dictionary<string, int> getBagSummary() { return this._manager.getBagSummary(); }
}
