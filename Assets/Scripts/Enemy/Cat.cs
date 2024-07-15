using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : Monster
{
    public GameObject laserEffect;

    bool useLaser = false;

    private void Start()
    {
        base.Start();

        player = PlayerMove.Instance.gameObject;
        attackCoolTimeCacl = attackCoolTime;
        StartCoroutine(ResetAtkArea());
    }

    private void Update()
    {
        base.Update();

        if(!useLaser)
        {
            transform.LookAt(player.transform.position);
        }
    }

    protected override IEnumerator Idle()
    {
        yield return null;

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

    protected override IEnumerator CalcCoolTime()
    {
        yield return null;

        while (true)
        {
            yield return null;
            if (!canAtk)
            {
                attackCoolTimeCacl -= Time.deltaTime;
                laserEffect.SetActive(true);

                if (attackCoolTimeCacl <= 0)
                {
                    attackCoolTimeCacl = attackCoolTime;
                    canAtk = true;
                }
            }
        }
    }

    protected override IEnumerator Attack()
    {
        yield return null;

        // ���� �÷��̾� ��ġ�� ����
        nvAgent.isStopped = true;
        nvAgent.SetDestination(player.transform.position);

        // ������ �߻�
        laserEffect.SetActive(true);

        yield return new WaitForSeconds(2);

        nvAgent.isStopped = false;
        nvAgent.speed = 0;
        canAtk = false;
        useLaser = true;

        yield return new WaitForSeconds(4);

        // ���� �ʱ�ȭ
        nvAgent.speed = moveSpeed;
        currentState = State.Idle;
        useLaser = false;
        laserEffect.SetActive(false);
        transform.LookAt(player.transform.position);

    }

    protected override IEnumerator Move()
    {
        yield return null;

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
}
