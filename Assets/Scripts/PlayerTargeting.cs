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
    public LayerMask targetLayer; // ���̾�
    public Transform nearestTarget; // ���� ����� ��ǥ
    public Vector3 scannerPos;
    public float scannerRadius;

    [Header("# Weapon")]
    public GameObject weapon; // ���� ������
    public Transform weaponStartPos;
    public float attackTime; // ���� �ֱ�
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

    // �迭 �� ���� ����� ���� ã�� �Լ�
    public Transform GetNearest(RaycastHit[] targets)
    {
        Transform result = null;
        float diff = 100; // ó�� ����� ���� �ּ� �Ÿ�

        // �νĵ� ������Ʈ���� �ÿ��̾���� �Ÿ� ���
        foreach (RaycastHit target in targets)
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
