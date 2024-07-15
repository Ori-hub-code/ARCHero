using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineManager : MonoBehaviour
{
    public GameObject[] slotSkillObject;
    public Button[] slot;

    public Sprite[] skillSprite;

    [System.Serializable]
    public class DisplayItemSlot
    {
        public List<Image> slotSprite = new List<Image>();
    }

    public DisplayItemSlot[] displayItemSlots;

    public Image displayResultImage;

    public List<int> startList = new List<int>();
    public List<int> resultIndexList = new List<int>();
    int itemCount = 3;

    private void Start()
    {
        for(int i = 0; i < itemCount * slot.Length; i++)
        {
            startList.Add(i);
        }

        for(int i = 0; i < slot.Length; i++)
        {
            for(int j = 0; j < itemCount; j++)
            {
                slot[i].interactable = false; // 클릭 비활성화

                int randomIndex = Random.Range(0, startList.Count);

                if(i == 0 && j == 1 || i == 1 && j == 0 || i == 2 && j == 2)
                {
                    resultIndexList.Add(startList[randomIndex]);
                }

                displayItemSlots[i].slotSprite[j].sprite = skillSprite[startList[randomIndex]];

                if(j == 0)
                {
                    displayItemSlots[i].slotSprite[itemCount].sprite = skillSprite[startList[randomIndex]];
                }

                startList.Remove(randomIndex);
            }
        }

        StartCoroutine(StartSlot(0, 6, 2));
        StartCoroutine(StartSlot(1, 10, 0));
        StartCoroutine(StartSlot(2, 14, 1));
    }

    IEnumerator StartSlot(int index, int multiplicationCount, int addCount)
    {
        for(int i = 0; i < (itemCount * multiplicationCount + addCount) * 2; i++)
        {
            slotSkillObject[index].transform.localPosition -= new Vector3(0, 50f, 0);

            if (slotSkillObject[index].transform.localPosition.y < 50f)
            {
                slotSkillObject[index].transform.localPosition += new Vector3(0, 300f, 0);
            }

            yield return new WaitForSeconds(0.03f);
        }

        for(int i = 0; i < itemCount; i++)
        {
            slot[i].interactable = true;
        }

    }

    public void ChooseSkill(int index)
    {
        displayResultImage.sprite = skillSprite[resultIndexList[index]];
    }
}
