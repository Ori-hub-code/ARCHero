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

        // 천사방
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
        // 중간 보스방
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
        // 메인 보스방
        else if (battleManager.stageCount == 20)
        {
            random = 27;
        }
        // 노말 방 ( 1 ~ 9 )
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
        // 노말 방 ( 11 ~ 19 )
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

        battleManager.roomIndexList.Add(random); // 중복 방지용으로 리스트에 값 추가
        battleManager.player.transform.position = battleManager.spawnPoints[random].position; // 플레이어 위치 이동

        // 해당 Room 의 Room Condition 의 Collider 활성화
        RoomCondition roomCondition = battleManager.rooms[random].GetComponentInChildren<RoomCondition>();
        roomCondition.GetComponent<Collider>().enabled = true;

        battleManager.roomIndex = random;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 스테이지 카운트 추가
        battleManager.stageCount++;

        // 모든 Room Condition 비활성화
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
