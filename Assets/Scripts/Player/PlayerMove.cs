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
    public JoyStick joyStick;
    PlayerTargeting targeting;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        targeting = GetComponent<PlayerTargeting>();
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
        if(joyStick.joyVec.x != 0 || joyStick.joyVec.y != 0)
        {
            rigid.velocity = new Vector3(joyStick.joyVec.x, 0, joyStick.joyVec.y).normalized * speed; // 좌표값

            // 회전값
            if (targeting.nearestTarget)
            {
                Transform target = targeting.nearestTarget.transform;

                transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
            }
            else
            {
                // 조이스틱 회전값
                rigid.rotation = Quaternion.Euler(0f, Mathf.Atan2(joyStick.joyVec.x, joyStick.joyVec.y) * Mathf.Rad2Deg, 0f);
            }

            // 애니메이션
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalk", true);
            anim.SetBool("isAttack", false);
        }
        else if(joyStick.joyVec.x == 0 && joyStick.joyVec.y == 0)
        {
            // 애니메이션
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalk", false);
            anim.SetBool("isAttack", false);

            // 회전값
            if (targeting.nearestTarget)
            {
                Transform target = targeting.nearestTarget.transform;

                transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Monster"))
        {
            Monster monsterLogic = collision.transform.GetComponent<Monster>();

            if (monsterLogic.currentState == MonsterMelleFSM.State.Attack)
            {
                PlayerHpBar.Instance.currentHp -= monsterLogic.damage * 2f;
                monsterLogic.currentState = MonsterMelleFSM.State.Idle;

                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Damaged"))
                {
                    anim.SetTrigger("damaged");
                    Instantiate(EffectSet.Instance.playerDmgEffect, targeting.nearestTarget.transform.position, Quaternion.Euler(90, 0, 0));
                }
            }
        }
    }
}
