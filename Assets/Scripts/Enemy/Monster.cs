using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonsterMelleFSM
{
    public GameObject enemyCanvas;
    public GameObject meleeAtkArea;

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

        attackCoolTime = 2f;
        attackCoolTimeCacl = attackCoolTime;

        attackRange = 3f;
        nvAgent.stoppingDistance = 1f;

        StartCoroutine(ResetAtkArea());
    }

    IEnumerator ResetAtkArea()
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

            if (enemyCanvas.GetComponent<EnemyHpBar>().currentHp <= 0)
            {
                transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
