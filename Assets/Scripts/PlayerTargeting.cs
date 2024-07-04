using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerTargeting : Singleton<PlayerTargeting>
{
    [Header("# Scanner")]
    public List<GameObject> monsterList;
    public List<GameObject> attackMonsterList;
    public LayerMask targetLayer; // ���̾�
    public Transform nearestTarget; // ���� ����� ��ǥ

    [Header("# Weapon")]
    public GameObject weapon; // ���� ������
    public Transform weaponStartPos;
    public float attackTime; // ���� �ֱ�
    public float currentAttackTime;

    private void Awake()
    {
        monsterList = new List<GameObject>();
        attackMonsterList = new List<GameObject>();
    }

    void Update()
    {
        if(attackMonsterList.Count > 0)
        {
            nearestTarget = GetNearest(attackMonsterList);

            if (nearestTarget != null)
            {
                currentAttackTime += Time.deltaTime;

                if (currentAttackTime > attackTime)
                {
                    currentAttackTime = 0;

                    // ���� ����
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

            bool isHit = Physics.Raycast(transform.position , monsterList[i].transform.position - transform.position, out hit, 20f, targetLayer);

            if (isHit && hit.transform.CompareTag("Monster"))
            {
                Gizmos.color = Color.green;

                if (!attackMonsterList.Contains(monsterList[i]))
                {
                    attackMonsterList.Add(monsterList[i]);
                }
            }
            else
            {
                Gizmos.color = Color.red;

                if (attackMonsterList.Contains(monsterList[i]))
                {
                    attackMonsterList.Remove(monsterList[i]);
                }
            }
            Gizmos.DrawRay(transform.position, monsterList[i].transform.position - transform.position);
        }
    }

    // �迭 �� ���� ����� ���� ã�� �Լ�
    public Transform GetNearest(List<GameObject> targets)
    {
        Transform result = null;
        float diff = 100; // ó�� ����� ���� �ּ� �Ÿ�

        // �νĵ� ������Ʈ���� �ÿ��̾���� �Ÿ� ���
        foreach (GameObject target in targets)
        {
            Vector3 myPos = transform.position; // �÷��̾� ��ġ
            Vector3 targetPos = target.transform.position; // �νĵ� ������Ʈ�� ��ġ
            float curDiff = Vector3.Distance(myPos, targetPos); // Distance(A,B) : ���� A �� B �� �Ÿ��� ������ִ� �Լ�

            if (curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }

        return result;
    }
}
