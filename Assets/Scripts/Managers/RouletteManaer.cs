using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteManaer : Singleton<RouletteManaer>
{
    public GameObject roulettePlate;
    public GameObject roulettePanel;
    public Transform needle;

    public Sprite[] skillSprite;
    public Image[] displayItemSlot;

    List<int> startList;
    List<int> resultIndexList;
    int itemCount;

    private void Awake()
    {
        startList = new List<int>();
        resultIndexList = new List<int>();
        itemCount = 6;
    }

    private void Start()
    {
        // 처음 이미지 세팅
        for(int i = 0; i < itemCount; i++)
        {
            startList.Add(i);
        }

        for(int i = 0; i < itemCount; i++)
        {
            int randomIndex = Random.Range(0, startList.Count);
            resultIndexList.Add(startList[randomIndex]);
            displayItemSlot[i].sprite = skillSprite[startList[randomIndex]];
            startList.Remove(randomIndex);
        }

        StartCoroutine(StartRoulette());
    }

    IEnumerator StartRoulette()
    {
        yield return new WaitForSeconds(2f);

        float randomSpeed = Random.Range(1.0f, 5.0f);
        float rotateSpeed = 100f * randomSpeed;

        while(true)
        {
            yield return null;

            if (rotateSpeed <= 0.01f) break;

            // 점점 속도 감소 (rotateSpeed -> 0 으로 감소)
            rotateSpeed = Mathf.Lerp(rotateSpeed, 0, Time.deltaTime * 2f);
            roulettePlate.transform.Rotate(0, 0, rotateSpeed);
        }

        yield return new WaitForSeconds(1f);

        Result();
    }

    void Result()
    {
        // Needle 과 Slot 의 거리를 비교해서 가장 가까운 Slot 찾기

        int closeIndex = -1;
        float closeDis = 500f;
        float currentDis = 0f;

        for(int i = 0; i < itemCount; i++)
        {
            currentDis = Vector2.Distance(displayItemSlot[i].transform.position, needle.position);

            if(closeDis > currentDis)
            {
                closeDis = currentDis;
                closeIndex = i;
            }
        }

        Debug.Log("Level Up Index : " + closeIndex);

        if(closeIndex == -1)
        {
            Debug.Log("Somethind is wrong!");
        }

        displayItemSlot[itemCount].sprite = displayItemSlot[closeIndex].sprite;

        Debug.Log("Level Up Index : " + resultIndexList[closeIndex]);
    }

}
