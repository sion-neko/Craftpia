using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class InputKey : MonoBehaviour
{
    KeyCode _key;
    Vector2 _moveVec;//[���A�c]


    protected Subject<KeyCode> _isKeyDown = new Subject<KeyCode>(); //�L�[�������ꂽ���Ƃ����m
    protected Subject<Vector2> _isDownWASD = new Subject<Vector2>();
    ReactiveProperty<bool> _isShiftKeyDown = new ReactiveProperty<bool>(); //�L�[�̒����������m

    public IObservable<KeyCode> OnKeyDown { get { return _isKeyDown; } }
    public IObservable<Vector2> OnDownWASD { get { return _isDownWASD; } }
    public IReadOnlyReactiveProperty<bool> OnShiftKeyDown { get { return _isShiftKeyDown; } }


    void Start()
    {
        //key����񉟂��ꂽ���m
        this.UpdateAsObservable()
            .Where(_ => Input.anyKeyDown)
            .Subscribe(_ =>
            {
                _key = SearchKey();
                _isKeyDown.OnNext(_key);
                Debug.Log(_key.ToString() + "��������܂����B");

            });

        //Shift������
        this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftShift))
            .Subscribe(_ => _isShiftKeyDown.Value = Input.GetKey(KeyCode.LeftShift));
        
        //���A�c����
        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                _moveVec.x = Input.GetAxis("Horizontal");
                _moveVec.y = Input.GetAxis("Vertical");
                _isDownWASD.OnNext(_moveVec);
            });


        //this.UpdateAsObservable()
        //    .Where(_ => Input.anyKey)
        //    .Subscribe(_ =>
        //    {
        //        foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
        //        {
        //            if (Input.GetKeyDown(code))
        //            {
        //                _key = code;
        //                break;
        //            }

        //        }
        //        OnLongKeyDown.Subscribe(_ => _key.ToString());
        //        Debug.Log(_key.ToString() + "��������܂����B");

        //    });



        //���������m�͂������ŁB���Ԃ���Ƃ��Ɋȗ���


    }



    //���L�[�������ꂽ�����f
    //Enum�̒l���w��̒l�ɂ���i���̓����B�j
    KeyCode SearchKey()
    {
        foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
        {
            //Debug.Log(code);
            if (Input.GetKeyDown(code))
            {
                return code;
            }
        }
        return KeyCode.None;
    }

}
