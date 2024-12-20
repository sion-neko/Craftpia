using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Walk : MonoBehaviour, IPlayerMove
{
    CharacterController con;
    private Animator anim;

    // ��(walk�����ׂ��ł͂Ȃ�)
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

        //Debug.Log("���������F" + walkVector);
        //if (walkVector.magnitude > 0)
        //{
        //    float angle = -1 * Mathf.Atan2(walkVector.x, -1 * walkVector.y) * Mathf.Rad2Deg;
        //    Debug.Log("���������F" + angle);
        //    transform.rotation = Quaternion.Euler(0, angle, 0);
        //}
        //characterController.Move(this.gameObject.transform.forward * MoveSpeed * Time.deltaTime);
        //Vector3 moveZ = new Vector3(0, 0, -walkVector.y);  //�@�O��i�J������j�@ 
        //Vector3 moveX = new Vector3(-walkVector.x, 0, 0); // ���E�i�J������j
        Vector3 moveZ = cameraForward * Input.GetAxis("Vertical") * 5;  //�@�O��i�J������j�@ 
        Vector3 moveX = Camera.main.transform.right * Input.GetAxis("Horizontal") * 5;

        // isGrounded �͒n�ʂɂ��邩�ǂ����𔻒肵�܂�
        // �n�ʂɂ���Ƃ��̓W�����v���\��
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
            // �d�͂���������
            moveDirection = moveZ + moveX + new Vector3(0, moveDirection.y, 0);
            //moveDirection.y -= gravity * Time.deltaTime;
        }

        // �v���C���[�̌�������͂̌����ɕύX�@
        transform.LookAt(transform.position + moveZ + moveX);

        // Move �͎w�肵���x�N�g�������ړ������閽��
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
