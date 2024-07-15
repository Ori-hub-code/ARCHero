using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepBolt : MonoBehaviour
{
    Rigidbody rigid;
    public Sheep parentMonster;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // 회전값
        Vector3 currentRotation = transform.rotation.eulerAngles;
        Vector3 parentRotation = parentMonster.transform.rotation.eulerAngles;
        currentRotation.y = parentRotation.y;
        transform.rotation = Quaternion.Euler(currentRotation);

        // 방향
        rigid.velocity = parentMonster.transform.forward * 6f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Wall") || collision.transform.CompareTag("Player") || collision.transform.CompareTag("Weapon"))
        {
            Destroy(gameObject);
        }
    }
}
