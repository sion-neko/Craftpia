using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BagTest : MonoBehaviour
{
    [SerializeField] Player bag;


    public void Start()
    {
        
    }
    public void ShowBag()
    {
        Debug.Log("�[�[�[�[�[�[�[�[�o�b�O�̒��g�[�[�[�[�[�[�[�[");
        foreach (var pare in bag.getBagSummary())
        {

            Debug.Log(pare.Key  + ":" + pare.Value);

        }
        Debug.Log("�[�[�[�[�[�[�[�[");
    }

    public void inItem()
    {
        Debug.Log("�J���[��1�o�b�O�ɓ��܂����B");
        bag.Cook("#100");
    }
    public void inItemISHI()    
    {
        Debug.Log("�΂̒ǉ�");
        bag.inItem("#000", 2);
    }
    public void inItemKI()
    {
        Debug.Log("�؂̒ǉ�");
        bag.inItem("#001", 2);
    }

    
}


