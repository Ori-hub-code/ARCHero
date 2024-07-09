using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject enemyCanvas;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {

            PlayerTargeting.Instance.nearestTarget = null;

            enemyCanvas.GetComponent<EnemyHpBar>().Damaged();

            if (enemyCanvas.GetComponent<EnemyHpBar>().currentHp <= 0)
            {
                transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
