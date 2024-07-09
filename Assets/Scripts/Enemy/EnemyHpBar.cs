using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    public Slider hpSlider;
    public Slider backHpSlide; // ����Ʈ �����̴�
    public bool backHpHit = false;

    public Transform enemy;
    public Vector3 hpBarPos;
    public float maxHp = 1000f;
    public float currentHp = 1000f;

    private void Awake()
    {
        hpSlider.value = 1;
        backHpSlide.value = 1;
    }

    private void Update()
    {
        transform.position = enemy.position + hpBarPos;

        // ���� ���� �Լ��� ����Ͽ� �ε巴�� �پ� �鵵�� ����
        hpSlider.value = Mathf.Lerp(hpSlider.value, currentHp / maxHp, Time.deltaTime * 5f);

        if(backHpHit)
        {
            backHpSlide.value = Mathf.Lerp(backHpSlide.value, currentHp / maxHp, Time.deltaTime * 10f);

            if(hpSlider.value >= backHpSlide.value - 0.01f)
            {
                backHpHit = false;
                backHpSlide.value = hpSlider.value;
            }
        }
    }

    public void Damaged()
    {
        currentHp -= 300f;
        Invoke("BackHpFun", 0.5f);
    }

    void BackHpFun()
    {
        backHpHit = true;
    }
}
