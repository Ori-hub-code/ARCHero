using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    public Transform player;
    public Slider hpBar;
    public Vector3 hpbarPos;
    public float maxHp;
    public float currentHp;

    public GameObject hpLineFolder;
    float unitHp = 200f;

    private void Update()
    {
        transform.position = player.position + hpbarPos;

        // 체력바 설정
        hpBar.value = currentHp / maxHp;
    }

    public void GetHpBoost()
    {
        maxHp += 150f;

        float scaleX = (1000f / unitHp) / (maxHp / unitHp);

        hpLineFolder.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(false);

        foreach(Transform child in hpLineFolder.transform)
        {
            child.gameObject.transform.localScale = new Vector3(scaleX, 1, 1);
        }

        hpLineFolder.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(true);
    }
}
