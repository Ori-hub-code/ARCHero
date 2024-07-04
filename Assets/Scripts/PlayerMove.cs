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

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();  
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");

        rigid.velocity = new Vector3(hAxis, 0, vAxis).normalized * speed; // 좌표값
        //rigid.rotation = Quaternion.LookRotation(new Vector3(0, ,0)); // 회전값


        // 애니메이션
        if(hAxis != 0 || vAxis != 0)
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalk", true);
        }
        else if(hAxis == 0 && vAxis == 0)
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalk", false);
        }
    }
}
