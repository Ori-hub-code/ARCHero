using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBase : MonoBehaviour
{
    // Hp
    public float maxHp = 1000f;
    public float curentHp = 1000f;

    // Damage
    public float damage = 100f;

    // Attack
    protected float playerRealizeRange = 10f; // 플레이어 인식 범위
    protected float attackRange = 5f; // 공격 범위
    protected float attackCoolTime = 5f;
    protected float attackCoolTimeCacl = 5f;
    protected bool canAtk = true;

    // Setting
    [SerializeField] protected float moveSpeed = 2f;
    [SerializeField] protected GameObject player;
    protected NavMeshAgent nvAgent;
    protected float distance;
    protected GameObject parentRoom;
}
