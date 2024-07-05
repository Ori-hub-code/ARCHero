using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    BattleManager battleManager;

    private void Awake()
    {
        battleManager = BattleManager.Instance;
    }

    void NextRoom()
    {
        int random = 0;

        // õ���
        if (battleManager.stageCount == 5 || battleManager.stageCount == 15)
        {
            random = Random.Range(21, 24);

            if(battleManager.roomIndexList.Contains(random))
            {
                while(battleManager.roomIndexList.Contains(random))
                {
                    random = Random.Range(21, 24);
                }
            }
        }
        // �߰� ������
        else if(battleManager.stageCount == 10)
        {
            random = Random.Range(24, 27);

            if (battleManager.roomIndexList.Contains(random))
            {
                while (battleManager.roomIndexList.Contains(random))
                {
                    random = Random.Range(24, 27);
                }
            }
        }
        // ���� ������
        else if (battleManager.stageCount == 20)
        {
            random = 27;
        }
        // �븻 �� ( 1 ~ 9 )
        else if(battleManager.stageCount >= 1 && battleManager.stageCount < 10 && battleManager.stageCount != 5)
        {
            random = Random.Range(1, 11);

            if (battleManager.roomIndexList.Contains(random))
            {
                while (battleManager.roomIndexList.Contains(random))
                {
                    random = Random.Range(1, 11);
                }
            }
        }
        // �븻 �� ( 11 ~ 19 )
        else if (battleManager.stageCount >= 11 && battleManager.stageCount < 20 && battleManager.stageCount != 15)
        {
            random = Random.Range(11, 21);

            if (battleManager.roomIndexList.Contains(random))
            {
                while (battleManager.roomIndexList.Contains(random))
                {
                    random = Random.Range(11, 21);
                }
            }
        }

        battleManager.roomIndexList.Add(random); // �ߺ� ���������� ����Ʈ�� �� �߰�
        battleManager.player.transform.position = battleManager.spawnPoints[random].position; // �÷��̾� ��ġ �̵�

        // �ش� Room �� Room Condition �� Collider Ȱ��ȭ
        RoomCondition roomCondition = battleManager.rooms[random].GetComponentInChildren<RoomCondition>();
        roomCondition.GetComponent<Collider>().enabled = true;

        battleManager.roomIndex = random;
    }

    private void OnTriggerEnter(Collider other)
    {
        // �������� ī��Ʈ �߰�
        battleManager.stageCount++;

        // ��� Room Condition ��Ȱ��ȭ
        foreach(GameObject room in battleManager.rooms)
        {
            RoomCondition roomCondition = room.GetComponentInChildren<RoomCondition>();
            roomCondition.GetComponent<Collider>().enabled = false;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            NextRoom();
        }
    }
}
