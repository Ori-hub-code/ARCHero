using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            PlayerTargeting.Instance.attackMonsterList.Remove(this.gameObject);
            PlayerTargeting.Instance.monsterList.Remove(this.gameObject);
            PlayerTargeting.Instance.nearestTarget = null;

            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            PlayerTargeting.Instance.attackMonsterList.Remove(this.gameObject);
            PlayerTargeting.Instance.monsterList.Remove(this.gameObject);
            PlayerTargeting.Instance.nearestTarget = null;

            Destroy(this.gameObject);
        }
    }
}
