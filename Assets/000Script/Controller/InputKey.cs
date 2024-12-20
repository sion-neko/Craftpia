using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class InputKey : MonoBehaviour
{
    KeyCode _key;
    Vector2 _moveVec;//[横、縦]


    protected Subject<KeyCode> _isKeyDown = new Subject<KeyCode>(); //キーが押されたことを検知
    protected Subject<Vector2> _isDownWASD = new Subject<Vector2>();
    ReactiveProperty<bool> _isShiftKeyDown = new ReactiveProperty<bool>(); //キーの長押しを検知

    public IObservable<KeyCode> OnKeyDown { get { return _isKeyDown; } }
    public IObservable<Vector2> OnDownWASD { get { return _isDownWASD; } }
    public IReadOnlyReactiveProperty<bool> OnShiftKeyDown { get { return _isShiftKeyDown; } }


    void Start()
    {
        //keyが一回押された検知
        this.UpdateAsObservable()
            .Where(_ => Input.anyKeyDown)
            .Subscribe(_ =>
            {
                _key = SearchKey();
                _isKeyDown.OnNext(_key);
                Debug.Log(_key.ToString() + "が押されました。");

            });

        //Shift長押し
        this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftShift))
            .Subscribe(_ => _isShiftKeyDown.Value = Input.GetKey(KeyCode.LeftShift));
        
        //横、縦入力
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
        //        Debug.Log(_key.ToString() + "が押されました。");

        //    });



        //長押し検知はこっちで。時間あるときに簡略化


    }



    //何キーが押されたか判断
    //Enumの値を指定の値にする（いつの日か。）
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
