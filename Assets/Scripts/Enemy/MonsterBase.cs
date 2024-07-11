using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBase : MonoBehaviour
{
    // Hp
    public float maxHp = 1000f;
    public float currentHp = 1000f;

    // Damage
    public float damage = 100f;

    // Attack
    [SerializeField] protected float playerRealizeRange = 10f; // 플레이어 인식 범위
    [SerializeField] protected float attackRange = 5f; // 공격 범위
    protected float attackCoolTime = 5f;
    [SerializeField] protected float attackCoolTimeCacl = 5f;
    [SerializeField] protected bool canAtk = true;

    // Setting
    [SerializeField] protected float moveSpeed = 2f;
    [SerializeField] protected GameObject player;
    [SerializeField] protected NavMeshAgent nvAgent;
    [SerializeField] protected float distance;
    [SerializeField] protected GameObject parentRoom;
    [SerializeField] LayerMask layerMask;

    protected bool CanAtkStateFun()
    {
        Vector3 targetDir = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);

        Physics.Raycast(new Vector3(transform.position.x, 0.5f, transform.position.z), targetDir, out RaycastHit hit, 30f, layerMask);
        distance = Vector3.Distance(player.transform.position, transform.position);

        if(hit.transform == null)
        {
            return false;
        }
        
        if(hit.transform.CompareTag("Player") && distance <= attackRange)
        {
            StartCoroutine(CalcCoolTime());
            return true;
        }
        else
        {
            return false;
        }
    }

    protected virtual IEnumerator CalcCoolTime()
    {
        while(true)
        {
            yield return null;
            if (!canAtk)
            {
                attackCoolTimeCacl -= Time.deltaTime;

                if(attackCoolTimeCacl <= 0)
                {
                    attackCoolTimeCacl = attackCoolTime;
                    canAtk = true;
                }
            }
        }
    }
}
