using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MonsterMelleFSM : MonsterBase
{
    public enum State { Idle, Move, Attack};
    protected bool startFSM = false;

    public State currentState = State.Idle;

    protected WaitForSeconds Delay500 = new WaitForSeconds(0.5f);
    protected WaitForSeconds Delay250 = new WaitForSeconds(0.25f);

    Animator anim;

    protected void Awake()
    {
        nvAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    protected void Start()
    {
        parentRoom = transform.parent.transform.parent.transform.parent.gameObject;
        roomCondition = parentRoom.GetComponentInChildren<RoomCondition>();
    }

    protected virtual void InitMonster()
    {
        
    }

    protected virtual IEnumerator FSM()
    {
        yield return null;

        foreach(GameObject enemy in PlayerTargeting.Instance.monsterList)
        {
            if(enemy == this.gameObject)
            {
                while (!roomCondition.playerInThisRoom)
                {
                    yield return Delay500;
                }

                InitMonster();

                while (true)
                {
                    yield return StartCoroutine(currentState.ToString());
                }
            }
        }
    }

    protected virtual IEnumerator Idle()
    {
        yield return null;

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            anim.SetBool("isIdle", true);
        }

        if (CanAtkStateFun())
        {
            if (canAtk)
            {
                currentState = State.Attack;
            }
            else
            {
                currentState = State.Idle;
                transform.LookAt(player.transform.position);
            }
        }
        else
        {
            currentState = State.Move;
        }

        currentState = State.Attack;
        currentState = State.Idle;
        currentState = State.Move;
    }

    // 공격 이펙트 함수
    protected virtual void AtkEffect()
    {

    }

    protected virtual IEnumerator Attack()
    {
        yield return null;

        // 현재 플레이어 위치를 저장
        nvAgent.stoppingDistance = 0;
        nvAgent.isStopped = true;
        nvAgent.SetDestination(player.transform.position);

        yield return Delay500;

        nvAgent.isStopped = false;
        nvAgent.speed = 30; // 돌진 스피드
        canAtk = false;

        // 애니메이션
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("stun"))
        {
            anim.SetTrigger("attack");
            anim.SetBool("isWalk", false);
            anim.SetBool("isIdle", false);
        }

        // 이펙트
        AtkEffect();

        yield return Delay500;

        // 세팅 초기화
        nvAgent.speed = moveSpeed;
        nvAgent.stoppingDistance = 3.5f;
        currentState = State.Idle;

        anim.SetBool("isWalk", false);
        anim.SetBool("isIdle", true);
    }

    protected virtual IEnumerator Move()
    {
        yield return null;


        // 애니메이션
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("walk"))
        {
            anim.SetBool("isWalk", true);
            anim.SetBool("isIdle", false);
        }

        if(CanAtkStateFun() && canAtk)
        {
            currentState = State.Attack;
        }
        else
        {
            nvAgent.SetDestination(player.transform.position);
        }
    }


}
