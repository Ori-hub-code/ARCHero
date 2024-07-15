using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinBolt : MonoBehaviour
{
    Rigidbody rigid;
    [SerializeField] Vector3 newDir;
    [SerializeField] int bounceCount = 3;
    public Penguin parentMonster;

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
        newDir = parentMonster.transform.forward * 6f;
        rigid.velocity = newDir;

        bounceCount = 3;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            bounceCount--;

            if (bounceCount > 0)
            {
                newDir = Vector3.Reflect(newDir, collision.contacts[0].normal);

                Debug.Log($"New Direction (after reflect): {newDir}");

                rigid.velocity = newDir;

                Debug.Log($"New Velocity (after reflect): {rigid.velocity}");
            }
            else
            {
                Destroy(gameObject);
            }
        }

        if (collision.transform.CompareTag("Player") || collision.transform.CompareTag("Weapon"))
        {
            Destroy(gameObject);
        }
    }

}
