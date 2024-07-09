using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    [Header("# Stage")]
    public GameObject player;
    public GameObject[] rooms;
    public Transform[] spawnPoints;
    public int roomIndex = 0; // 방 번호 저장을 위한 변수
    public int stageCount = 0; // 현재 스테이지 카운트
    public List<int> roomIndexList;

    private void Awake()
    {
        // 리스트 초기화
        roomIndexList = new List<int>();

        // 시작 위치 세팅
        player.transform.position = spawnPoints[0].position;

        // 리스트에 숫자 0 입력
        roomIndexList.Add(roomIndex);
    }

    private void Update()
    {
        if(PlayerTargeting.Instance.monsterList.Count == 0)
        {
            Portal portal = rooms[roomIndex].GetComponentInChildren<Portal>();

            portal.GetComponent<Collider>().enabled = true;
        }
    }
}
