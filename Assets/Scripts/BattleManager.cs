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
    public int roomIndex = 0; // �� ��ȣ ������ ���� ����
    public int stageCount = 0; // ���� �������� ī��Ʈ
    public List<int> roomIndexList;

    private void Awake()
    {
        // ����Ʈ �ʱ�ȭ
        roomIndexList = new List<int>();

        // ���� ��ġ ����
        player.transform.position = spawnPoints[0].position;

        // ����Ʈ�� ���� 0 �Է�
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
