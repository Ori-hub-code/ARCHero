using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();  
    }

    private void OnEnable()
    {
        rigid.velocity = transform.forward * 20f;

        // È¸Àü°ª
        //Vector3 dir = (PlayerTargeting.Instance.nearestTarget.position - transform.position).normalized;
        //transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Name : " + other.transform.name);
        
        if(other.transform.CompareTag("Wall") | other.transform.CompareTag("Monster"))
        {
            //Debug.Log("Name : " + other.transform.name);
            rigid.velocity = Vector3.zero;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Name : " + collision.transform.name);

        if (collision.transform.CompareTag("Wall") | collision.transform.CompareTag("Monster"))
        {
            //Debug.Log("Name : " + collision.transform.name);
            rigid.velocity = Vector3.zero;
            Destroy(gameObject);
        }
    }
}
