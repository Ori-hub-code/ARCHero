using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penguin : Monster
{
    public GameObject enemyBolt;
    LineRenderer lineRenderer;

    private void Awake()
    {
        base.Awake();

        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        base.Start();

        lineRenderer.startColor = new Color(1, 0, 0, 0.5f);
        lineRenderer.endColor = new Color(1, 0, 0, 0.5f);
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;

        player = PlayerMove.Instance.gameObject;
        attackCoolTimeCacl = attackCoolTime;
        StartCoroutine(ResetAtkArea());
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

    protected override IEnumerator Attack()
    {
        yield return null;

        // 현재 플레이어 위치를 저장
        nvAgent.isStopped = true;
        nvAgent.SetDestination(player.transform.position);

        // 위험 표시
        DangerMarkerShoot();

        yield return Delay500;

        // 공격
        Shoot();

        nvAgent.isStopped = false;
        nvAgent.speed = 0;
        canAtk = false;

        // 이펙트
        AtkEffect();

        yield return Delay500;

        // 세팅 초기화
        nvAgent.speed = moveSpeed;
        currentState = State.Idle;
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

    IEnumerator WaitPlayer()
    {
        yield return null;

        while (!roomCondition.playerInThisRoom)
        {
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(0.5f);
        transform.LookAt(player.transform.position);
        DangerMarkerShoot();

        yield return new WaitForSeconds(2f);
        transform.LookAt(player.transform.position);
        DangerMarkerDeactive();
        Shoot();

    }

    public void DangerMarkerShoot()
    {
        Debug.Log("Marker");
        Vector3 newPos = weaponPos.transform.position;
        Vector3 newDir = transform.forward;
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);

        for(int i = 1; i < 4; i++)
        {
            Physics.Raycast(newPos, newDir, out RaycastHit hit, 30f, LayerMask.GetMask("Wall"));
            //Debug.Log($"name : {hit.transform.name} position : {hit.point}");

            lineRenderer.positionCount++;
            //Debug.Log(" position : " + hit.point);
            lineRenderer.SetPosition(i, hit.point);

            newPos = hit.point;
            newDir = Vector3.Reflect(newDir, hit.normal);
        }

    }

    public void DangerMarkerDeactive()
    {
        for(int i = 0; i < lineRenderer.positionCount; i++)
        {
            Debug.Log("Set Vector3.zero");
            lineRenderer.SetPosition(i, Vector3.zero);
        }
        lineRenderer.positionCount = 0;
    }

    public void Shoot()
    {
        GameObject bolt = Instantiate(enemyBolt, weaponPos.transform.position, enemyBolt.transform.rotation);
        bolt.GetComponent<PenguinBolt>().parentMonster = this;
    }
}
