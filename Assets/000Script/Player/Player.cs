using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    [SerializeField] GameObject playerLevelUpText;
    [SerializeField] TextMeshProUGUI playerLevelText;
    
    
    private Animator anim;

    // ゲームクリアメニュー表示
    [SerializeField] GameObject gameClearMenu;
    // ゲームオーバーメニュー表示
    [SerializeField] GameObject gameOverMenu;
    

    int walkSpeed;
    


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
        //Time.timeScale = 1;
        anim = GetComponent<Animator>();


    }
    public void inItem(string id, int quantity = 1)
    {
        _manager.pickUpItem(id, quantity);
        // コインを拾ったらゲームクリア
        if (id == "#500")
        {
            Debug.Log("ゲームクリア！");
            if (!gameClearMenu.activeSelf)
            {
                anim.SetTrigger("clearTrigger");
                gameClearMenu.SetActive(true);
                StartCoroutine(FadeIn(gameClearMenu.GetComponent<CanvasGroup>()));
                
            }
        }
    }

    public void Cook(string cookItem_id)
    {
        _manager.doCook(cookItem_id);
    }

    public IEnumerator PlayerLevelUp()
    {
        PlayerLevelData playerLevelData = GameData.instance.getPlayerLevelData(playerLevel);
        foreach (Sozai sozai in playerLevelData.nextLevelRequaimets)
        {
            if (!_manager.existSozai(sozai))
            {
                Debug.Log("素材が足りません");

                yield break;
            }
        }
        foreach (Sozai sozai in playerLevelData.nextLevelRequaimets)
        {
            _manager.consumeSozai(sozai);
        }
        playerLevel++;
        playerHP.setHP(playerLevelData.status.hp);
        staminaSlider.maxValue = playerHP.getHP();
        staminaSlider.value = playerHP.getHP();
        walkSpeed = playerLevelData.status.speed;

        //Text levelText = playerLevelUpText.transform.Find("LevelText").gameObject.GetComponent<TextMe
        playerLevelText.text = (playerLevel - 1) + " → " + playerLevel;
        playerLevelUpText.SetActive(true);
        yield return new WaitForSeconds(3);

        playerLevelUpText.SetActive(false);
    }

    public void Walk(Vector2 walkVector)
    {
        if (walkVector.magnitude > 0)
        {
            // 歩くたびにHPを減らす。
            if (playerHP.ConsumeHP(Config.CONSUME_HP_SPEED))
            {
                beforeIsZero = false;
                playerMove.walk(walkVector * walkSpeed);
                staminaSlider.value = playerHP.getHP();
            }
            else
            {
                Debug.Log("体力０");
                if (!gameOverMenu.activeSelf)
                {
                    anim.SetBool("death", true);
                    gameOverMenu.SetActive(true);
                    StartCoroutine(FadeIn(gameOverMenu.GetComponent<CanvasGroup>()));
                    // Time.timeScale = 0f;  // 動いているほうが楽しいのでいったん時間は止めないことにする。
                }

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
    public bool existSozai(Sozai sozai) { return this._manager.existSozai(sozai); }


    

    private void OnEnable()
    {
        
    }

    IEnumerator FadeIn(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0;

        float time = 0;
        float duration = 1f;

        while (time < duration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = time / duration;
            yield return null;
        }

        canvasGroup.alpha = 1;
    }
}
