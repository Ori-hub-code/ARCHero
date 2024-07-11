using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCondition : MonoBehaviour
{
    List<GameObject> monsterList;
    public bool playerInThisRoom = false;
    public bool isClearRoom = false;

    PlayerTargeting playerTargeting;

    private void Awake()
    {
        monsterList = new List<GameObject>();
    }

    private void Start()
    {
        playerTargeting = PlayerTargeting.Instance;
    }

    private void Update()
    {
        if(playerInThisRoom)
        {
            if(monsterList.Count <= 0 && !isClearRoom)
            {
                isClearRoom = true;
                Debug.Log("Clear");
            }
        }
    }

    void PlayerEnter()
    {
        playerInThisRoom = true;
        playerTargeting.monsterList = monsterList;
        Debug.Log("Enter New Room! Mob Count : " + playerTargeting.monsterList.Count);
        Debug.Log("Player Enter New Room");

        // �÷��̾� ���� ��, �ش� ���� ��Ż�� Collider ��Ȱ��ȭ
        Portal portal = BattleManager.Instance.rooms[BattleManager.Instance.roomIndex].GetComponentInChildren<Portal>();
        portal.GetComponent<Collider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("PlayerEnter", 0.1f);
        }

        if (other.CompareTag("Monster"))
        {
            if(!monsterList.Contains(other.gameObject))
            {
                monsterList.Add(other.gameObject);
            }
            Debug.Log("Monster Name : " + other.gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInThisRoom = false;
            Debug.Log("Player Exit");
        }

        if (other.CompareTag("Monster"))
        {
            monsterList.Remove(other.gameObject);
        }
    }
}
