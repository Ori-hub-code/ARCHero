using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : Monster
{
    public GameObject dangerMarker;
    public GameObject enemyBolt;


    protected override IEnumerator Idle()
    {
        yield return null;

        //if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        //{
        //    anim.SetBool("isIdle", true);
        //}

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

    protected override IEnumerator Attack()
    {
        yield return null;

        // 현재 플레이어 위치를 저장
        nvAgent.isStopped = true;
        nvAgent.SetDestination(player.transform.position);

        // 위험 표시
        DangerMarkShoot();

        yield return Delay500;

        Shoot();

        nvAgent.isStopped = false;
        nvAgent.speed = 0;
        canAtk = false;

        // 애니메이션
        //if (!anim.GetCurrentAnimatorStateInfo(0).IsName("stun"))
        //{
        //    anim.SetTrigger("attack");
        //    anim.SetBool("isWalk", false);
        //    anim.SetBool("isIdle", false);
        //}

        // 이펙트
        AtkEffect();

        yield return Delay500;

        // 세팅 초기화
        nvAgent.speed = moveSpeed;
        currentState = State.Idle;

        //anim.SetBool("isWalk", false);
        //anim.SetBool("isIdle", true);
    }

    protected override IEnumerator Move()
    {
        yield return null;


        // 애니메이션
        //if (!anim.GetCurrentAnimatorStateInfo(0).IsName("walk"))
        //{
        //    anim.SetBool("isWalk", true);
        //    anim.SetBool("isIdle", false);
        //}

        if (CanAtkStateFun() && canAtk)
        {
            Debug.Log("State Attack");
            currentState = State.Attack;
        }
        else
        {
            nvAgent.SetDestination(player.transform.position);
            transform.LookAt(player.transform.position);
        }
    }

    IEnumerator WaitPlayer()
    {
        yield return null;

        while(!roomCondition.playerInThisRoom)
        {
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(0.5f);
        transform.LookAt(player.transform.position);
        
    }

    void DangerMarkShoot()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        Physics.Raycast(newPosition, transform.forward, out RaycastHit hit, 30f, LayerMask.GetMask("Wall"));

        if (hit.transform.CompareTag("Wall"))
        {
            GameObject dangerMarkerClone = Instantiate(dangerMarker, newPosition, transform.rotation);
            dangerMarkerClone.GetComponent<DangerLine>().endPostion = hit.point;
        }
    }

    void Shoot()
    {
        GameObject bolt = Instantiate(enemyBolt, weaponPos.transform.position, enemyBolt.transform.rotation);
        bolt.GetComponent<SheepBolt>().parentMonster = this;
    }
}
