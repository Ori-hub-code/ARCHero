using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerTargeting : Singleton<PlayerTargeting>
{
    Animator anim;
    PlayerMove playerMove;

    [Header("# Scanner")]
    public List<GameObject> monsterList;
    public List<GameObject> attackMonsterList;
    public LayerMask targetLayer; // ���̾�
    public Transform nearestTarget; // ���� ����� ��ǥ

    [Header("# Weapon")]
    public GameObject weapon; // ���� ������
    public Transform weaponStartPos;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();

        monsterList = new List<GameObject>();
        attackMonsterList = new List<GameObject>();
    }

    void Update()
    {
        if(attackMonsterList.Count > 0)
        {
            nearestTarget = GetNearest(attackMonsterList);

            if (nearestTarget != null && playerMove.joyStick.joyVec.x == 0 && playerMove.joyStick.joyVec.y == 0)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") || anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                {
                    // �ִϸ��̼�
                    anim.SetBool("isWalk", false);
                    anim.SetBool("isIdle", false);
                    anim.SetBool("isAttack", true);

                    //StartCoroutine(ThrowWeapon());
                }
            }
        }
    }

    IEnumerator ThrowWeapon()
    {
        //yield return new WaitForSeconds(0.8f);
        yield return null;

        // �ִϸ��̼� �ӵ�
        anim.SetFloat("attackSpeed", 1);

        // ���� ����
        Instantiate(weapon, weaponStartPos);
    }

    void FinishiAttack()
    {
        // �ִϸ��̼�
        anim.SetBool("isWalk", false);
        anim.SetBool("isIdle", true);
        anim.SetBool("isAttack", false);
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
