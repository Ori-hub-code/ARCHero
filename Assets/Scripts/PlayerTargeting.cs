using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerTargeting : Singleton<PlayerTargeting>
{
    [Header("# Scanner")]
    public List<GameObject> monsterList;
    public RaycastHit[] targets;
    public LayerMask targetLayer; // 레이어
    public Transform nearestTarget; // 가장 가까운 목표
    public Vector3 scannerPos;
    public float scannerRadius;

    [Header("# Weapon")]
    public GameObject weapon; // 무기 프리팹
    public Transform weaponStartPos;
    public float attackTime; // 공격 주기
    public float currentAttackTime;

    private void Awake()
    {
        monsterList = new List<GameObject>();
    }

    void Update()
    {
        if(monsterList.Count > 0)
        {
            targets = Physics.SphereCastAll((transform.position + scannerPos), scannerRadius, transform.rotation.eulerAngles, 100f, targetLayer);

            nearestTarget = GetNearest(targets);

            if (nearestTarget != null)
            {
                currentAttackTime += Time.deltaTime;

                if (currentAttackTime > attackTime)
                {
                    currentAttackTime = 0;

                    // 무기 생성
                    Instantiate(weapon, weaponStartPos);
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        for(int i = 0; i < monsterList.Count; i++)
        {
            RaycastHit hit;

            bool isHit = Physics.Raycast(transform.position , monsterList[i].transform.position - transform.position, out hit, 20f, LayerMask.GetMask("Monster") | LayerMask.GetMask("Wall"));

            if (isHit && hit.transform.CompareTag("Monster"))
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.red;
            }
            Gizmos.DrawRay(transform.position, monsterList[i].transform.position - transform.position);
        }

        Gizmos.DrawWireSphere(transform.position + scannerPos, scannerRadius);
    }

    // 배열 내 가장 가까운 적을 찾는 함수
    public Transform GetNearest(RaycastHit[] targets)
    {
        Transform result = null;
        float diff = 100; // 처음 계산을 위한 최소 거리

        // 인식된 오브젝트마다 플에이어와의 거리 계산
        foreach (RaycastHit target in targets)
        {
            Vector3 myPos = transform.position; // 플레이어 위치
            Vector3 targetPos = target.transform.position; // 인식된 오브젝트의 위치
            float curDiff = Vector3.Distance(myPos, targetPos); // Distance(A,B) : 벡터 A 와 B 의 거리를 계산해주는 함수

            if (curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }

        return result;
    }
}
