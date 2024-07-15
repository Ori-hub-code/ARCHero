using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonsterMelleFSM
{
    public GameObject enemyCanvas;
    public GameObject weaponPos;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerRealizeRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void Start()
    {
        base.Start();

        player = PlayerMove.Instance.gameObject;

        attackCoolTimeCacl = attackCoolTime;

        StartCoroutine(ResetAtkArea());
    }

    protected void Update()
    {
        foreach (GameObject enemy in PlayerTargeting.Instance.monsterList)
        {
            if (enemy == this.gameObject)
            {
                nvAgent.SetDestination(player.transform.position);

                if (startFSM == false)
                {
                    StartCoroutine(FSM());
                    startFSM = true;
                }
            }
        }
    }

    protected override void InitMonster()
    {
        base.InitMonster();
        maxHp += (BattleManager.Instance.stageCount + 1) * 100f;
        currentHp = maxHp;
        damage += (BattleManager.Instance.stageCount + 1) * 10f;
    }

    protected override void AtkEffect()
    {
        base.AtkEffect();
        Instantiate(EffectSet.Instance.duckAtkEffect, transform.position, Quaternion.Euler(90, 0, 0));
    }

    protected IEnumerator ResetAtkArea()
    {
        while (true)
        {
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {

            PlayerTargeting.Instance.nearestTarget = null;

            enemyCanvas.GetComponent<EnemyHpBar>().Damaged();

            // ¿Ã∆Â∆Æ
            Instantiate(EffectSet.Instance.duckDmgEffect, collision.contacts[0].point, Quaternion.Euler(90, 0, 0));

            if (enemyCanvas.GetComponent<EnemyHpBar>().currentHp <= 0)
            {
                transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
