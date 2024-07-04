using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTargeting : Singleton<PlayerTargeting>
{
    public List<GameObject> monsterList;

    [Header("# Scanner")]
    public LayerMask targetLayer; // ���̾�
    public Transform nearestTarget; // ���� ����� ��ǥ

    private void Awake()
    {
        monsterList = new List<GameObject>();
    }

    void Update()
    {
        if(monsterList.Count > 0 )
        {
            nearestTarget = GetNearest(monsterList);
        }
    }

    void OnDrawGizmos()
    {
        for(int i = 0; i < monsterList.Count; i++)
        {
            RaycastHit hit;

            bool isHit = Physics.Raycast(transform.position , monsterList[i].transform.position - transform.position, out hit, 20f, targetLayer);
        
            if(isHit && hit.transform.CompareTag("Monster"))
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.red;
            }
            Gizmos.DrawRay(transform.position, monsterList[i].transform.position - transform.position);
        }
    }

    // �迭 �� ���� ����� ���� ã�� �Լ� (�̵�)
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
