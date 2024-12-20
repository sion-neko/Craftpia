using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour, IPlayerAction, IPlayerBagController
{
    IOno _ono;
    [SerializeField] GameData gamedata;
    IManager _manager;
    [SerializeField] IPlayerMove playerMove;
    

    // �ЂƂO��walkVector��ۑ�����B
    private bool beforeIsZero = false;



    private void Start()
    {
        _ono = new Ono(1, 1);
        _manager = new PlayerManager(gamedata);
        playerMove = GetComponent<Walk>();
    }
    public void inItem(string id, int quantity = 1)
    {
        _manager.pickUpItem(id, quantity);
    }

    public void Cook(string cookItem_id)
    {
        _manager.doCook(cookItem_id);
    }

    public void Walk(Vector2 walkVector)
    {
        if (walkVector.magnitude > 0)
        {
            playerMove.walk(walkVector);
            beforeIsZero = false;
        }
        else
        {
            // �A����vector��0�̏ꍇ�X���[��������
            if (!beforeIsZero)
            {
                // �v���C���[���~�߂Ȃ��Ƃ����Ȃ��̂�
                // 1��͏��������s����B
                playerMove.walk(walkVector);
                beforeIsZero = true;
            }

        }

    }

    public void UseItem()
    {
        
    }


    public int getPlayerOnoLv() { return _ono.getLv(); }

    public int getPlayerOnoAtk() { return _ono.getAtk(); }

    public Dictionary<string, int> getBagSummary() { return this._manager.getBagSummary(); }
}
