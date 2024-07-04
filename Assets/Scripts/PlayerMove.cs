using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : Singleton<PlayerMove>
{
    [Header("# Player")]
    Rigidbody rigid;
    Animator anim;

    [Header("# Move")]
    [SerializeField] float hAxis;
    [SerializeField] float vAxis;
    [SerializeField] float speed;

    [Header("# Scripts")]
    JoyStick joyStick;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();  
    }

    private void Start()
    {
        joyStick = JoyStick.Instance;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        // hAxis = Input.GetAxis("Horizontal");
        // vAxis = Input.GetAxis("Vertical");

        if(joyStick.joyVec.x != 0 || joyStick.joyVec.y != 0)
        {
            rigid.velocity = new Vector3(joyStick.joyVec.x, 0, joyStick.joyVec.y).normalized * speed; // 좌표값
            rigid.rotation = Quaternion.Euler(0f, Mathf.Atan2(joyStick.joyVec.x, joyStick.joyVec.y) * Mathf.Rad2Deg,0f); ; // 회전값
        }


        // 애니메이션
        if(joyStick.joyVec.x != 0 || joyStick.joyVec.y != 0)
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalk", true);
        }
        else if(joyStick.joyVec.x == 0 && joyStick.joyVec.y == 0)
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalk", false);
        }
    }
}
