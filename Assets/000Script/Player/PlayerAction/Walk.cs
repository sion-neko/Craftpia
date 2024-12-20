using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Walk : MonoBehaviour, IPlayerMove
{
    CharacterController con;
    private Animator anim;

    // 仮(walkが持つべきではない)
    private int _walkSpeed;


    Vector3 moveDirection = Vector3.zero;
    private void Start()
    {
        anim = GetComponent<Animator>();
        con = GetComponent<CharacterController>();
    }

    public void walk(Vector2 walkVector)
    {

        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        //Debug.Log("歩く方向：" + walkVector);
        //if (walkVector.magnitude > 0)
        //{
        //    float angle = -1 * Mathf.Atan2(walkVector.x, -1 * walkVector.y) * Mathf.Rad2Deg;
        //    Debug.Log("向く方向：" + angle);
        //    transform.rotation = Quaternion.Euler(0, angle, 0);
        //}
        //characterController.Move(this.gameObject.transform.forward * MoveSpeed * Time.deltaTime);
        //Vector3 moveZ = new Vector3(0, 0, -walkVector.y);  //　前後（カメラ基準）　 
        //Vector3 moveX = new Vector3(-walkVector.x, 0, 0); // 左右（カメラ基準）
        Vector3 moveZ = cameraForward * Input.GetAxis("Vertical") * 5;  //　前後（カメラ基準）　 
        Vector3 moveX = Camera.main.transform.right * Input.GetAxis("Horizontal") * 5;

        // isGrounded は地面にいるかどうかを判定します
        // 地面にいるときはジャンプを可能に
        if (con.isGrounded)
        {
            moveDirection = moveZ + moveX;
            //if (Input.GetButtonDown("Jump"))
            //{
            //    moveDirection.y = jump;
            //}
        }
        else
        {
            // 重力を効かせる
            moveDirection = moveZ + moveX + new Vector3(0, moveDirection.y, 0);
            //moveDirection.y -= gravity * Time.deltaTime;
        }

        // プレイヤーの向きを入力の向きに変更　
        transform.LookAt(transform.position + moveZ + moveX);

        // Move は指定したベクトルだけ移動させる命令
        con.Move(moveDirection * Time.deltaTime);

        if (walkVector.magnitude > 0)
        {
            anim.SetBool("walking", true);
            anim.speed = 1.7f;

        }
        else
        {
            anim.SetBool("walking", false);
            anim.speed = 1.0f;
        }


    }

    public void run() { }


}
